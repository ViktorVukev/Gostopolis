namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Gostopolis.Services.Identity;
using Infrastructure;
using Models.Restaurants;
using Services.Restaurants;

using static Constants;

/// <summary>
/// Responds to user's requests related to the restaurants.
/// </summary>
/// <param name="restaurants">Service interface that keeps restaurants business logic.</param>
/// <param name="currentUser">Current user service interface.</param>
public class RestaurantsController(
    IRestaurantService restaurants,
    ICurrentUserService currentUser) 
    : ApiController
{
    /// <summary>
    /// Used to display all details of a restaurant.
    /// </summary>
    /// <param name="id">The unique key of the restaurant.</param>
    /// <returns>A model with the details of a restaurant.</returns>
    [HttpGet]
    [Route(Id)]
    public async Task<Result<RestaurantDetailsOutputModel>> Details(int id)
        => await restaurants.FindById(id);

    /// <summary>
    /// Used to display all details of a restaurant and all available offers for the given criteria.
    /// </summary>
    /// <param name="id">The unique key of the restaurant.</param>
    /// <param name="model">Time period for the reservation and part of name or the town of the restaurant.</param>
    /// <returns>A model with the details of a restaurant.</returns>
    [HttpGet]
    [Route(Id + PathSeparator + nameof(Offers))]
    public async Task<Result<RestaurantDetailsOutputModel>> Offers(
        int id, 
        [FromQuery] SearchRestaurantsInputModel model)
        => await restaurants.FindById(id, model);

    /// <summary>
    /// Used to show a list of all restaurants in the platform.
    /// </summary>
    /// <returns>A collection of all restaurants.</returns>
    [HttpGet]
    [Route(nameof(All))]
    [AuthorizeAdministrator]
    public Result<IEnumerable<RestaurantDetailsOutputModel>> All()
        => Result<IEnumerable<RestaurantDetailsOutputModel>>
            .SuccessWith(restaurants.GetAll());

    /// <summary>
    /// List of all approved and opened restaurants in the platform that are available for the given period.
    /// </summary>
    /// <param name="model">Time period for the reservation and part of name or the town of the restaurant.</param>
    /// <returns>List of the restaurants that are available.</returns>
    [HttpGet]
    public Result<IEnumerable<RestaurantDetailsOutputModel>> Listing(
        [FromQuery] SearchRestaurantsInputModel model) 
        => Result<IEnumerable<RestaurantDetailsOutputModel>>
            .SuccessWith(restaurants.GetListing(model));

    /// <summary>
    /// List of all approved and opened restaurants in the platform which match the filter.
    /// </summary>
    /// <param name="model">Options that are used to filter the restaurants.</param>
    /// <returns>List of the filtered restaurants.</returns>
    [HttpGet]
    [Route(nameof(Filter))]
    public Result<IEnumerable<RestaurantDetailsOutputModel>> Filter(
        [FromQuery] FilterRestaurantsInputModel model)
        => Result<IEnumerable<RestaurantDetailsOutputModel>>
            .SuccessWith(restaurants.Filter(model));

    /// <summary>
    /// Get's current user's restaurants.
    /// </summary>
    /// <returns>A list of current user's restaurants.</returns>
    [HttpGet]
    [Route(nameof(Mine))]
    [Authorize(Roles = PartnerRoleName)]
    public Result<IEnumerable<RestaurantDetailsOutputModel>> Mine()
        => Result<IEnumerable<RestaurantDetailsOutputModel>>
            .SuccessWith(restaurants.ByCurrentUser());

    /// <summary>
    /// Creates restaurant in the platform if all the data in the model is valid.
    /// </summary>
    /// <param name="input">All data required to create a restaurant in the platform.</param>
    /// <returns>Whether the restaurant is successfully created or not.</returns>
    [HttpPost]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result<int>> Create(
        CreateRestaurantInputModel input)
        => await restaurants.Create(input);

    [HttpPut]
    [Route(Id)]
    [Authorize(Roles = "Partner")]
    public async Task<ActionResult> Edit(int id, EditRestaurantInputModel input)
    {
        var restaurant = await restaurants.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != restaurant.PartnerId)
        {
            return BadRequest("You are not allowed to edit this property.");
        }

        return Ok(await restaurants.Edit(id, input));
    }

    /// <summary>
    /// Allows administrator to approve a property to be listed in the platform.
    /// </summary>
    /// <param name="id">The unique key of the restaurant.</param>
    /// <returns>Whether the restaurant is approved successfully or not.</returns>
    [HttpPut]
    [Route(nameof(Approve) + PathSeparator + Id)]
    [AuthorizeAdministrator]
    public async Task<Result> Approve(int id)
    {
        var isApprovedSuccessfully = await restaurants.Approve(id);

        return isApprovedSuccessfully 
            ? Result.Success 
            : Error.CommonError;
    }

    /// <summary>
    /// Allows partners to change the visibility of any of their restaurants and administrators all of them.
    /// </summary>
    /// <param name="id">The unique key of the restaurant.</param>
    /// <returns>Whether the visibility of the restaurant is updated successfully or not.</returns>
    [HttpPut]
    [Route(nameof(ChangeVisibility) + PathSeparator + Id)]
    [Authorize(Roles = AdministratorAndPartner)]
    public async Task<Result> ChangeVisibility(int id)
    {
        var restaurant = await restaurants.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != restaurant.PartnerId)
        {
            return Error.PropertyForbidden;
        }

        var isUpdatedSuccessfully = await restaurants.ChangeVisibility(id);

        return isUpdatedSuccessfully 
            ? Result.Success 
            : Error.CommonError;
    }

    /// <summary>
    /// Allows partners to delete any of their restaurants and administrators all of them.
    /// </summary>
    /// <param name="id">The unique key of the restaurant.</param>
    /// <returns>Whether the restaurant is deleted successfully or not.</returns>
    [HttpDelete]
    [Route(Id)]
    [Authorize(Roles = AdministratorAndPartner)]
    public async Task<Result> Delete(int id)
    {
        var restaurant = await restaurants.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != restaurant.PartnerId)
        {
            return Error.PropertyForbidden;
        }

        var isDeletedSuccessfully = await restaurants.Delete(id);

        return isDeletedSuccessfully 
            ? Result.Success 
            : Error.CommonError;
    }
}
