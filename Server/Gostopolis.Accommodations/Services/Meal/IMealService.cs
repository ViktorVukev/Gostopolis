namespace Gostopolis.Accommodations.Services.Meal;

using Models.Meals;

public interface IMealService
{
    Task<int> Create(CreateMealInputModel input);

    IEnumerable<MealDetailsOutputModel> ByAccommodationId(int id);

    Task<MealDetailsOutputModel> FindById(int id);

    //Task<bool> Edit(int id, EditAccommodationInputModel input);

    Task<bool> Delete(int id);
}
