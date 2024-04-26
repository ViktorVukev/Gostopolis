namespace Gostopolis.Accommodations.Services.Accommodations;

using AutoMapper;
using Data;
using Data.Models;
using Gostopolis.Data.Models;
using Messages.Partners;
using Gostopolis.Services;
using Gostopolis.Services.Files;
using Gostopolis.Services.Identity;
using Gostopolis.Services.Properties;
using Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Models.Accommodations;
using Models.Rooms;
using Schedule;
using System.Linq;

public class AccommodationService(
        AccommodationsDbContext db,
        ICurrentUserService currentUser,
        IPropertyService properties,
        IFileService fileService,
        IScheduleService schedule,
        IMapper mapper,
        IPublishEndpoint publisher)
    : DataService<Accommodation>(db),
        IAccommodationService
{
    public async Task<int> Create(CreateAccommodationInputModel input)
    {
        var documentUrl = await fileService.UploadFileAsync(input.OwnershipDocumentBase64);
        var coverPhotoUrl = await fileService.UploadFileAsync(input.CoverPhotoBase64);

        var accommodation = new Accommodation()
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

        var messageData = new AccommodationCreatedMessage
        {
            AccommodationId = accommodation.Id,
            Name = accommodation.Name
        };

        var message = new Message(messageData);

        await this.Save(accommodation, message);

        await publisher.Publish(messageData);

        await this.MarkMessageAsPublished(message.Id);

        return accommodation.Id;
    }

    public IEnumerable<AccommodationDetailsOutputModel> GetAll()
        => mapper.ProjectTo<AccommodationDetailsOutputModel>(this.All());

    public IEnumerable<AccommodationDetailsOutputModel> GetListing(SearchAccommodationsInputModel model)
    {
        var accommodations = mapper
            .ProjectTo<AccommodationDetailsOutputModel>(this.All()
                .Where(a =>
                    (a.Town == model.Town || a.Name.Contains(model.Town) || a.Address.Contains(model.Town))
                    && a.IsTemporaryClosed == false))
            .ToList();

        foreach (var accommodation in accommodations)
        {
            accommodation.RoomConfigurations = this.GetRoomConfigurations(accommodation, model.People, model.Rooms, model.StartDate, model.EndDate);

        }

        return accommodations
            .Where(a => a.RoomConfigurations.Any());
    }

    public IEnumerable<AccommodationDetailsOutputModel> Filter(FilterAccommodationsInputModel model)
    {
        var facilities = model.Facilities.Split(", ");

        var accommodations = mapper
            .ProjectTo<AccommodationDetailsOutputModel>(this.All()
                .Where(a =>
                    (a.Town == model.Town || a.Name.Contains(model.Town) || a.Address.Contains(model.Town))
                    && a.IsTemporaryClosed == false)
                .Where(a =>
                    model.Types.Split().Select(int.Parse).Contains(a.TypeId)
                    && model.Stars.Split().Select(int.Parse).Contains(a.Stars)
                    && a.HasParking == model.HasParking
                    && a.HasPosTerminal == model.HasPosTerminal
                    && a.AcceptPets == model.AcceptPets
                    && facilities.All(f => a.Facilities.Contains(f))))
            .ToList();

        foreach (var accommodation in accommodations)
        {
            accommodation.RoomConfigurations = this.GetRoomConfigurations(accommodation, model.People, model.Rooms, model.StartDate, model.EndDate);
        }

        accommodations = accommodations
            .Where(a =>
            {
                var startTime = new DateTime(model.StartDate, TimeOnly.MinValue);
                var endTime = new DateTime(model.EndDate, TimeOnly.MinValue);

                return a.RoomConfigurations.Any(rc =>
                    rc.Sum(r => r.PricePerNight) * (endTime - startTime).Days >= model.MinPrice
                    && rc.Sum(r => r.PricePerNight) * (endTime - startTime).Days <= model.MaxPrice);
            })
            .ToList();

        return accommodations
            .Where(a => a.RoomConfigurations.Any());
    }

    public IEnumerable<AccommodationDetailsOutputModel> ByCurrentUser()
        => mapper.ProjectTo<AccommodationDetailsOutputModel>(this.All().Where(a => a.PartnerId == currentUser.UserId));

    public async Task<AccommodationDetailsOutputModel> FindById(int id, SearchAccommodationsInputModel model)
    {
        var accommodation = await mapper
            .ProjectTo<AccommodationDetailsOutputModel>(this
                .All()
                .Where(r => r.Id == id))
            .FirstOrDefaultAsync();

        accommodation.RoomConfigurations = this.GetRoomConfigurations(accommodation, model.People, model.Rooms, model.StartDate, model.EndDate);

        return accommodation;
    }

    public async Task<AccommodationDetailsOutputModel> FindById(int id)
        => await mapper
            .ProjectTo<AccommodationDetailsOutputModel>(this
                .All()
                .Where(r => r.Id == id))
            .FirstOrDefaultAsync();

    public async Task<bool> Edit(int id, EditAccommodationInputModel input)
    {
        var accommodation = await this.All().SingleOrDefaultAsync(a => a.Id == id);
        if (accommodation == null)
        {
            return false;
        }

        accommodation.Name = input.Name;
        accommodation.TypeId = input.TypeId;
        accommodation.IdentificationNumber = input.IdentificationNumber;
        accommodation.Address = input.Address;
        accommodation.Latitude = input.Latitude;
        accommodation.Longitude = input.Longitude;
        accommodation.Town = input.Town;
        accommodation.LocationUrl = input.LocationUrl;
        accommodation.Stars = input.Stars;
        accommodation.HasParking = input.HasParking;
        accommodation.HasPosTerminal = input.HasPosTerminal;
        accommodation.AcceptOnlinePayments = input.AcceptOnlinePayments;
        accommodation.AcceptPets = input.AcceptPets;
        accommodation.SpokenLanguages = input.SpokenLanguages;
        accommodation.PhoneNumber = input.PhoneNumber;
        accommodation.Description = input.Description;
        accommodation.Facilities = input.Facilities;
        accommodation.CheckInTime = TimeOnly.TryParseExact(input.CheckInTime, "HH:mm", out var citime) ? citime : null;
        accommodation.CheckOutTime = TimeOnly.TryParseExact(input.CheckOutTime, "HH:mm", out var cotime) ? cotime : null;
        accommodation.IsVerified = false;

        if (input.OwnershipDocumentBase64 != null)
        {
            accommodation.OwnershipDocumentUrl = await fileService.UploadFileAsync(input.OwnershipDocumentBase64);
        }


        if (input.CoverPhotoUrl != null)
        {
            accommodation.CoverPhotoUrl = input.CoverPhotoUrl;
        }

        await this.Save(accommodation);

        return true;
    }

    public async Task<bool> Approve(int id)
    {
        var accommodation = await this.All().SingleOrDefaultAsync(a => a.Id == id);

        accommodation.IsVerified = true;

        await this.Data.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeVisibility(int id)
    {
        var accommodation = await this.All().SingleOrDefaultAsync(a => a.Id == id);

        if (accommodation.IsVerified == false && accommodation.IsTemporaryClosed == false)
        {
            return false;
        }

        accommodation.IsTemporaryClosed = !accommodation.IsTemporaryClosed;

        await this.Data.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var accommodation = await this.FindById(id);

        this.Data.Remove(accommodation);

        await this.Data.SaveChangesAsync();

        return true;
    }

    private List<List<RoomDetailsOutputModel>> GetRoomConfigurations(AccommodationDetailsOutputModel accommodation, int people, int rooms, DateOnly startDate, DateOnly endDate)
    {
        var configurations = new List<List<RoomDetailsOutputModel>>();

        properties.GetAccommodatingCombinations(accommodation.Rooms, people, rooms)
            .ForEach(combination =>
            {
                var totalPrice = 0m;

                var currentCombination = combination.Split().Select(int.Parse);

                var configurationRooms = new List<RoomDetailsOutputModel>();

                foreach (var currentElement in currentCombination)
                {
                    var room = accommodation.Rooms.FirstOrDefault(r =>
                        r.Capacity == currentElement
                        && !configurationRooms.Contains(r)
                        && schedule
                            .CheckAvailability(new CheckAvailabilityInputModel()
                            {
                                RoomId = r.Id,
                                StartDate = startDate.ToDateTime((TimeOnly)accommodation.CheckInTime),
                                EndDate = endDate.ToDateTime((TimeOnly)accommodation.CheckOutTime)
                            })
                            .GetAwaiter()
                            .GetResult());

                    if (room != null)
                    {
                        configurationRooms.Add(room);

                        var endTime = new DateTime(endDate, TimeOnly.MinValue);
                        var startTime = new DateTime(startDate, TimeOnly.MinValue);

                        totalPrice += room.PricePerNight * (endTime - startTime).Days;
                    }
                }

                if (configurationRooms.Count == rooms)
                {
                    accommodation.CheapestOption ??= totalPrice;

                    if (totalPrice < accommodation.CheapestOption)
                    {
                        accommodation.CheapestOption = totalPrice;
                    }

                    configurations.Add(configurationRooms);
                }
            });

        return configurations;
    }
}
