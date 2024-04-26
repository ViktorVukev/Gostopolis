namespace Gostopolis.Restaurants.Services.Restaurants;

using Models.Restaurants;

public interface IRestaurantService
{
    Task<int> Create(CreateRestaurantInputModel input);

    IEnumerable<RestaurantDetailsOutputModel> GetAll();

    IEnumerable<RestaurantDetailsOutputModel> GetListing(SearchRestaurantsInputModel input);

    IEnumerable<RestaurantDetailsOutputModel> Filter(FilterRestaurantsInputModel input);

    IEnumerable<RestaurantDetailsOutputModel> ByCurrentUser();

    Task<RestaurantDetailsOutputModel> FindById(int id);

    Task<RestaurantDetailsOutputModel> FindById(int id, SearchRestaurantsInputModel input);

    Task<bool> Edit(int id, EditRestaurantInputModel input);

    Task<bool> Approve(int id);

    Task<bool> ChangeVisibility(int id);

    Task<bool> Delete(int id);
}
