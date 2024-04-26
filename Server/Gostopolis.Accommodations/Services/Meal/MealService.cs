namespace Gostopolis.Accommodations.Services.Meal;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Gostopolis.Accommodations.Data.Models;
using Data;
using Gostopolis.Services;
using Models.Meals;

public class MealService(
        AccommodationsDbContext db,
        IMapper mapper)
    : DataService<Meal>(db),
        IMealService
{
    public async Task<int> Create(CreateMealInputModel input)
    {
        var meal = new Meal()
        {
            Name = input.Name,
            StartTime = TimeOnly.TryParseExact(input.StartTime, "HH:mm", out var stime) ? stime : TimeOnly.MinValue,
            EndTime = TimeOnly.TryParseExact(input.EndTime, "HH:mm", out var etime) ? etime : TimeOnly.MinValue, 
            AccommodationId = input.AccommodationId
        };

        await this.Save(meal);

        return meal.Id;
    }

    public IEnumerable<MealDetailsOutputModel> ByAccommodationId(int id)
        => mapper.ProjectTo<MealDetailsOutputModel>(this.All().Where(r => r.AccommodationId == id));

    public async Task<MealDetailsOutputModel> FindById(int id)
        => await mapper
            .ProjectTo<MealDetailsOutputModel>(this
                .All()
                .Where(m => m.Id == id))
            .FirstOrDefaultAsync();

    //public async Task<bool> Edit(int id, EditAccommodationInputModel input)
    //{
    //    var accommodation = await this.All().SingleOrDefaultAsync(a => a.Id == id);
    //    if (accommodation == null)
    //    {
    //        return false;
    //    }

    //    accommodation.IsVerified = false;
    //    accommodation.IdentificationNumber = input.IdentificationNumber;

    //    if (input.CoverPhotoBase64.StartsWith("base64"))
    //    {
    //        accommodation.CoverPhotoUrl = await fileService.UploadFileAsync(input.CoverPhotoBase64);
    //    }


    //    await this.Save(accommodation);

    //    return true;
    //}

    public async Task<bool> Delete(int id)
    {
        var meal = await this.All().SingleOrDefaultAsync(m => m.Id == id);

        if (meal != null)
        {
            this.Data.Remove(meal);
        }

        await this.Data.SaveChangesAsync();

        return true;
    }
}
