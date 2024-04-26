namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Infrastructure;
using Models.RestaurantTypes;
using Services.RestaurantType;

using static Constants;

/// <summary>
/// Used to manage restaurant types.
/// </summary>
/// <param name="restaurantTypes">Service interface that keeps restaurant types business logic.</param>
public class RestaurantTypesController(
        IRestaurantTypeService restaurantTypes)
    : ApiController
{
    /// <summary>
    /// Allows the administrator to create restaurant type.
    /// </summary>
    /// <param name="input">Restaurant type's name and description.</param>
    /// <returns>The id of the newly created restaurant type.</returns>
    [HttpPost]
    [AuthorizeAdministrator]
    public async Task<Result<int>> Create(RestaurantTypeInputModel input) 
        => await restaurantTypes.Create(input);

    /// <summary>
    /// Gets all restaurant types from the database.
    /// </summary>
    /// <returns>List of all restaurant types.</returns>
    [HttpGet]
    public async Task<Result<IEnumerable<RestaurantTypeOutputModel>>> All()
        => Result<IEnumerable<RestaurantTypeOutputModel>>
            .SuccessWith(await restaurantTypes.GetAll());

    /// <summary>
    /// Updates restaurant type details.
    /// </summary>
    /// <param name="id">The restaurant type's id.</param>
    /// <param name="input">The restaurant type's new details.</param>
    /// <returns>Whether the restaurant type is successfully updated or not.</returns>
    [HttpPut]
    [Route(Id)]
    [AuthorizeAdministrator]
    public async Task<Result> Edit(int id, RestaurantTypeInputModel input)
    {
        var isSucceeded = await restaurantTypes.Edit(id, input);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }

    /// <summary>
    /// Deletes restaurant type from the database.
    /// </summary>
    /// <param name="id">The restaurant type's id.</param>
    /// <returns>Whether the restaurant type is successfully deleted or not.</returns>
    [HttpDelete]
    [Route(Id)]
    [AuthorizeAdministrator]
    public async Task<Result> Delete(int id)
    {
        var isSucceeded = await restaurantTypes.Delete(id);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }
}
