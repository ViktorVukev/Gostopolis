namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Gostopolis.Services.Identity;
using Models.Tables;
using Services.Restaurants;
using Services.Tables;

using static Constants;

/// <summary>
/// Used to manage restaurants' tables.
/// </summary>
/// <param name="restaurants">Service interface that keeps restaurants business logic.</param>
/// <param name="currentUser">Current user service interface.</param>
/// <param name="tables">Service interface that keeps tables business logic.</param>
public class TablesController(
        IRestaurantService restaurants,
        ICurrentUserService currentUser,
        ITableService tables) 
    : ApiController
{
    /// <summary>
    /// Creates a table in the database.
    /// </summary>
    /// <param name="input">Table details.</param>
    /// <returns>The id of the newly created table.</returns>
    [HttpPost]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result<int>> Create(CreateTableInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.TablesForbidden;
        }

        return await tables.Create(input);
    }

    /// <summary>
    /// Gets table details by given id.
    /// </summary>
    /// <param name="id">The id of the table.</param>
    /// <returns>The table details.</returns>
    [HttpGet]
    [Route(Id)]
    public async Task<Result<TableDetailsOutputModel>> Details(int id)
    {
        var table = await tables.FindById(id);
        if (table == null)
        {
            return Error.CommonError;
        }

        return table;
    }

    /// <summary>
    /// Updates the table's details.
    /// </summary>
    /// <param name="input">Contains table's new details.</param>
    /// <returns>Whether the table is successfully deleted or not.</returns>
    [HttpPut]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Update(EditTableInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.TablesForbidden;
        }

        var isSucceeded = await tables.Update(input);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }

    /// <summary>
    /// Deletes the table with the given id.
    /// </summary>
    /// <param name="id">The id of the table.</param>
    /// <returns>Whether the table is successfully deleted or not.</returns>
    [HttpDelete]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Delete(int id)
    {
        var restaurant = restaurants
            .GetAll()
            .Single(r => r.Tables
                .Any(t => t.Id == id));

        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.TablesForbidden;
        }

        var isSucceeded = await tables.Delete(id);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }
}
