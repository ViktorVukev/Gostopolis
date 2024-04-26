namespace Gostopolis.Restaurants.Services.Restaurants;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Data.Models;
using Gostopolis.Services;
using Gostopolis.Services.Emails;
using Gostopolis.Services.Files;
using Gostopolis.Services.Identity;
using Gostopolis.Services.Properties;
using MassTransit;
using Messages.Partners;
using Microsoft.EntityFrameworkCore;
using Models.Restaurants;
using Models.Tables;
using Models.WorkingTimes;
using Schedule;
using static Constants.Email;

public class RestaurantService(
    RestaurantsDbContext db,
    ICurrentUserService currentUser,
    IFileService fileService,
    IEmailService emailService,
    IPropertyService properties,
    IScheduleService schedule,
    IMapper mapper,
    IPublishEndpoint publisher)
    : DataService<Restaurant>(db),
    IRestaurantService
{
    public async Task<int> Create(CreateRestaurantInputModel input)
    {
        var documentUrl = await fileService.UploadFileAsync(input.OwnershipDocumentBase64);
        var coverPhotoUrl = await fileService.UploadFileAsync(input.CoverPhotoBase64);

        var restaurant = new Restaurant()
        {
            Name = input.Name,
            TypeId = input.TypeId,
            IdentificationNumber = input.IdentificationNumber,
            OwnershipDocumentUrl = documentUrl,
            CoverPhotoUrl = coverPhotoUrl,
            Address = input.Address,
            Latitude = input.Latitude,
            Longitude = input.Longitude,
            Town = input.Town,
            LocationUrl = input.LocationUrl,
            Stars = input.Stars,
            HasParking = input.HasParking,
            HasPosTerminal = input.HasPosTerminal,
            AcceptOnlinePayments = input.AcceptOnlinePayments,
            AcceptPets = input.AcceptPets,
            SpokenLanguages = input.SpokenLanguages,
            PhoneNumber = input.PhoneNumber,
            Description = input.Description,
            PartnerId = currentUser.UserId
        };

        var messageData = new RestaurantCreatedMessage
        {
            RestaurantId = restaurant.Id,
            Name = restaurant.Name
        };

        var message = new Message(messageData);

        await this.Save(restaurant, message);

        await emailService.Send(PropertyCreatedTitle, PropertyCreatedContent, currentUser.Email);

        await publisher.Publish(messageData);

        await this.MarkMessageAsPublished(message.Id);

        return restaurant.Id;
    }

    public IEnumerable<RestaurantDetailsOutputModel> GetAll()
        => mapper.ProjectTo<RestaurantDetailsOutputModel>(All());

    public IEnumerable<RestaurantDetailsOutputModel> GetListing(SearchRestaurantsInputModel model)
    {
        var restaurants = mapper
            .ProjectTo<RestaurantDetailsOutputModel>(this.All()
                .Where(r =>
                    (r.Town == model.Town || r.Name.Contains(model.Town) || r.Address.Contains(model.Town))
                    && r.IsTemporaryClosed == false))
            .ToList();

        foreach (var restaurant in restaurants)
        {
            restaurant.TableConfigurations = this.GetTableConfigurations(restaurant, model.People, model.Tables, model.StartTime);

        }

        return restaurants
            .Where(r => this.CheckWorkingTime(model.StartTime, r.WorkingTime))
            .Where(r => r.TableConfigurations.Any());
    }

    public IEnumerable<RestaurantDetailsOutputModel> Filter(FilterRestaurantsInputModel model)
    {
        var restaurants = mapper
            .ProjectTo<RestaurantDetailsOutputModel>(this.All()
                .Where(r =>
                    (r.Town == model.Town || r.Name.Contains(model.Town) || r.Address.Contains(model.Town))
                    && r.IsTemporaryClosed == false)
                .Where(r =>
                    model.Types.Split().Select(int.Parse).Contains(r.TypeId)
                    && model.Stars.Split().Select(int.Parse).Contains(r.Stars)
                    && r.HasParking == model.HasParking
                    && r.HasPosTerminal == model.HasPosTerminal
                    && r.AcceptPets == model.AcceptPets))
            .ToList();

        foreach (var restaurant in restaurants)
        {
            restaurant.TableConfigurations = this.GetTableConfigurations(restaurant, model.People, model.Tables, model.StartTime);
        }

        return restaurants
            .Where(r => this.CheckWorkingTime(model.StartTime, r.WorkingTime))
            .Where(r => r.TableConfigurations.Any());
    }

    public IEnumerable<RestaurantDetailsOutputModel> ByCurrentUser()
        => mapper.ProjectTo<RestaurantDetailsOutputModel>(this.All().Where(r => r.PartnerId == currentUser.UserId));

    public async Task<RestaurantDetailsOutputModel> FindById(int id, SearchRestaurantsInputModel model)
    {
        var restaurant = await mapper
            .ProjectTo<RestaurantDetailsOutputModel>(this
                .All()
                .Where(r => r.Id == id))
            .FirstOrDefaultAsync();

        restaurant.TableConfigurations = this.GetTableConfigurations(restaurant, model.People, model.Tables, model.StartTime);

        return restaurant;
    }

    public async Task<RestaurantDetailsOutputModel> FindById(int id)
        => await mapper
            .ProjectTo<RestaurantDetailsOutputModel>(this
                .All()
                .Where(r => r.Id == id))
            .FirstOrDefaultAsync();

    public async Task<bool> Edit(int id, EditRestaurantInputModel input)
    {
        var restaurant = await this.All().FirstOrDefaultAsync(r => r.Id == id);

        restaurant.Name = input.Name;
        restaurant.TypeId = input.TypeId;
        restaurant.IdentificationNumber = input.IdentificationNumber;
        restaurant.Address = input.Address;
        restaurant.Latitude = input.Latitude;
        restaurant.Longitude = input.Longitude;
        restaurant.Town = input.Town;
        restaurant.LocationUrl = input.LocationUrl;
        restaurant.Stars = input.Stars;
        restaurant.HasParking = input.HasParking;
        restaurant.HasPosTerminal = input.HasPosTerminal;
        restaurant.AcceptOnlinePayments = input.AcceptOnlinePayments;
        restaurant.AcceptPets = input.AcceptPets;
        restaurant.SpokenLanguages = input.SpokenLanguages;
        restaurant.PhoneNumber = input.PhoneNumber;
        restaurant.Description = input.Description;
        restaurant.IsVerified = false;

        if (input.OwnershipDocumentBase64 != null)
        {
            restaurant.OwnershipDocumentUrl = await fileService.UploadFileAsync(input.OwnershipDocumentBase64);
        }

        if (input.CoverPhotoUrl != null)
        {
            restaurant.CoverPhotoUrl = input.CoverPhotoUrl;
        }

        await this.Save(restaurant);

        return true;
    }

    public async Task<bool> Approve(int id)
    {
        var restaurant = await All().SingleOrDefaultAsync(r => r.Id == id);

        restaurant.IsVerified = true;

        await Data.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeVisibility(int id)
    {
        var restaurant = await All().SingleOrDefaultAsync(r => r.Id == id);

        if (restaurant.IsVerified == false && restaurant.IsTemporaryClosed == false)
        {
            return false;
        }
        
        restaurant.IsTemporaryClosed = !restaurant.IsTemporaryClosed;

        await Data.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var restaurant = await All().SingleOrDefaultAsync(r => r.Id == id);

        if (restaurant != null)
        {
            Data.Remove(restaurant);
        }

        await Data.SaveChangesAsync();

        return true;
    }

    private bool CheckWorkingTime(DateTime startTime, WorkingTimeOutputModel? workingTime)
    {
        return startTime.DayOfWeek switch
        {
            DayOfWeek.Monday => workingTime.MondayOpenTime != null &&
                                workingTime.MondayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                workingTime.MondayCloseTime > TimeOnly.FromDateTime(startTime),
            DayOfWeek.Tuesday => workingTime.TuesdayOpenTime != null &&
                                 workingTime.TuesdayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                 workingTime.TuesdayCloseTime > TimeOnly.FromDateTime(startTime),
            DayOfWeek.Wednesday => workingTime.WednesdayOpenTime != null &&
                                   workingTime.WednesdayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                   workingTime.WednesdayCloseTime > TimeOnly.FromDateTime(startTime),
            DayOfWeek.Thursday => workingTime.ThursdayOpenTime != null &&
                                  workingTime.ThursdayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                  workingTime.ThursdayCloseTime > TimeOnly.FromDateTime(startTime),
            DayOfWeek.Friday => workingTime.FridayOpenTime != null &&
                                workingTime.FridayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                workingTime.FridayOpenTime > TimeOnly.FromDateTime(startTime),
            DayOfWeek.Saturday => workingTime.SaturdayOpenTime != null &&
                                  workingTime.SaturdayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                  workingTime.SaturdayCloseTime > TimeOnly.FromDateTime(startTime),
            DayOfWeek.Sunday => workingTime.SundayOpenTime != null &&
                                workingTime.SundayOpenTime < TimeOnly.FromDateTime(startTime) &&
                                workingTime.SundayCloseTime > TimeOnly.FromDateTime(startTime),
            _ => false
        };
    }

    private List<List<TableDetailsOutputModel>> GetTableConfigurations(RestaurantDetailsOutputModel restaurant, int people, int tables, DateTime startTime)
    {
        var configurations = new List<List<TableDetailsOutputModel>>();

        properties.GetAccommodatingCombinations(restaurant.Tables, people, tables)
            .ForEach(combination =>
            {
                var currentCombination = combination.Split().Select(int.Parse);

                var configurationTables = new List<TableDetailsOutputModel>();

                foreach (var currentElement in currentCombination)
                {
                    var table = restaurant.Tables.FirstOrDefault(t =>
                        t.Capacity == currentElement
                        && !configurationTables.Contains(t)
                        && schedule
                            .CheckAvailability(new CheckAvailabilityInputModel()
                            {
                                TableId = t.Id,
                                StartDate = startTime,
                                EndDate = startTime.AddHours(2)
                            })
                            .GetAwaiter()
                            .GetResult());

                    if (table != null)
                    {
                        configurationTables.Add(table);
                    }
                }

                if (configurationTables.Count == tables)
                {

                    configurations.Add(configurationTables);
                }
            });

        return configurations;
    }
}