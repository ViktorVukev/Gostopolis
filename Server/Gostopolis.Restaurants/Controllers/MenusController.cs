namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Gostopolis.Services.Identity;
using Models.Menus;
using Services.Menus;
using Services.Restaurants;

using static Constants;

/// <summary>
/// Used to interact with menu records.
/// </summary>
/// <param name="restaurants">Service interface that keeps restaurants business logic.</param>
/// <param name="currentUser">Current user service interface.</param>
/// <param name="menus">Service interface that keeps menus business logic.</param>
public class MenusController(
        IRestaurantService restaurants,
        ICurrentUserService currentUser,
        IMenuService menus) 
    : ApiController
{
    /// <summary>
    /// Creates a new menu in a restaurant.
    /// </summary>
    /// <param name="input">The menu name, restaurant id and products ids.</param>
    /// <returns>The id of the newly created menu.</returns>
    [HttpPost]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result<int>> Create(CreateMenuInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.MenusForbidden;
        }

        return await menus.Create(input);
    }

    /// <summary>
    /// Updates the menu's name.
    /// </summary>
    /// <param name="input">Contains menu's name</param>
    /// <returns>Whether the menu is successfully deleted or not.</returns>
    [HttpPut]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Update(EditMenuInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.MenusForbidden;
        }

        var isSucceeded = await menus.Update(input);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }

    /// <summary>
    /// Deletes the menu with the given id.
    /// </summary>
    /// <param name="id">The id of the menu.</param>
    /// <returns>Whether the menu is successfully deleted or not.</returns>
    [HttpDelete]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Delete(int id)
    {
        var restaurant = restaurants
            .GetAll()
            .Single(r => r.Menus
                .Any(m => m.Id == id));

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.MenusForbidden;
        }

        var isSucceeded = await menus.Delete(id);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }
}
