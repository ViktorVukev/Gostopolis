namespace Gostopolis.Admin.Services.Restaurants;

using Gostopolis.Services;
using Models.Restaurants;
using Models.RestaurantTypes;
using Refit;

public interface IRestaurantService
{
    [Get("/Restaurants/{id}")]
    Task<Result<RestaurantDetailsOutputModel>> Details(int id);

    [Get("/Restaurants/All")]
    Task<Result<IEnumerable<RestaurantDetailsOutputModel>>> All();

    [Post("/RestaurantTypes")]
    Task<Result<int>> CreateRestaurantType(RestaurantTypeInputModel model);

    [Get("/RestaurantTypes")]
    Task<Result<IEnumerable<RestaurantTypeOutputModel>>> RestaurantTypes();

    [Put("/RestaurantTypes/{id}")]
    Task<Result> EditRestaurantType(int id, RestaurantTypeInputModel model);

    [Delete("/RestaurantTypes/{id}")]
    Task<Result> DeleteRestaurantType(int id);

    [Put("/Restaurants/Approve/{id}")]
    Task<Result> Approve(int id);

    [Delete("/Restaurants/{id}")]
    Task<Result> Delete(int id);
}
