namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Gostopolis.Services.Identity;
using Models.Products;
using Services.Products;
using Services.Restaurants;

using static Constants;

/// <summary>
/// Used to create, update and delete products from properties.
/// </summary>
/// <param name="restaurants">Service interface that keeps restaurants business logic.</param>
/// <param name="currentUser">Current user service interface.</param>
/// <param name="products">Service interface that keeps products business logic.</param>
public class ProductsController(
        IRestaurantService restaurants,
        ICurrentUserService currentUser,
        IProductService products) 
    : ApiController
{

    /// <summary>
    /// Creates a new product in a restaurant.
    /// </summary>
    /// <param name="input">Product details and restaurant and menu ids.</param>
    /// <returns>The id of the newly created product.</returns>
    [HttpPost]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result<int>> Create(CreateProductInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.ProductsForbidden;
        }

        return await products.Create(input);
    }

    /// <summary>
    /// Updates the menu's name.
    /// </summary>
    /// <param name="input">Contains menu's name</param>
    /// <returns>Whether the menu is successfully deleted or not.</returns>
    [HttpPut]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Update(EditProductInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.ProductsForbidden;
        }

        var isSucceeded = await products.Update(input);

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
        var isSucceeded = await products.Delete(id);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }
}
