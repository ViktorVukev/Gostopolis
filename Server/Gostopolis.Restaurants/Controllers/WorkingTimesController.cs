namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Gostopolis.Services.Identity;
using Models.WorkingTimes;
using Services.Restaurants;
using Services.WorkingTimes;

using static Constants;

/// <summary>
/// Used to manage restaurants' working hours.
/// </summary>
/// <param name="restaurants">Service interface that keeps restaurants business logic.</param>
/// <param name="currentUser">Current user service interface.</param>
/// <param name="workingTimes">Service interface that keeps working hours business logic.</param>
public class WorkingTimesController(
        IRestaurantService restaurants,
        ICurrentUserService currentUser,
        IWorkingTimeService workingTimes)
    : ApiController
{
    /// <summary>
    /// The property owner can set its working hours.
    /// </summary>
    /// <param name="input">The time period a restaurant is open each day.</param>
    /// <returns>The id of the working hours.</returns>
    [HttpPost]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result<int>> Create(WorkingTimeInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);
        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.WorkingHoursForbidden;
        }

        return await workingTimes.Create(input);
    }

    /// <summary>
    /// The property owner can change its working hours.
    /// </summary>
    /// <param name="input">The time period a restaurant is open each day.</param>
    /// <returns>The id of the working hours.</returns>
    [HttpPut]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Edit(WorkingTimeInputModel input)
    {
        var restaurant = await restaurants.FindById(input.RestaurantId);
        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.WorkingHoursForbidden;
        }

        var isSucceeded = await workingTimes.Edit(input);

        return isSucceeded
            ? Result.Success
            : Error.CommonError;
    }

    /// <summary>
    /// The property owner can delete its working hours.
    /// </summary>
    /// <param name="id">The id of the working hours of the restaurant.</param>
    /// <returns>Whether the working hours.</returns>
    [HttpDelete]
    [Authorize(Roles = PartnerRoleName)]
    public async Task<Result> Delete(int id)
    {
        var restaurant = restaurants.GetAll()
            .Single(r => r.WorkingTime?.Id == id);
        
        if (restaurant.PartnerId != currentUser.UserId)
        {
            return Error.WorkingHoursForbidden;
        }

        var isSucceeded = await workingTimes.Delete(id);

        return isSucceeded 
            ? Result.Success 
            : Error.CommonError;
    }
}