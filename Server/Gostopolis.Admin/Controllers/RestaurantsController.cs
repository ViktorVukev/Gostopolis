using Microsoft.AspNetCore.Mvc;

namespace Gostopolis.Admin.Controllers;

using AutoMapper;
using Models.Applications;
using Services.Identity;
using Services.Restaurants;

public class RestaurantsController(
    IRestaurantService restaurants,
    IIdentityService users,
    IMapper mapper) : AdministrationController
{
    public async Task<IActionResult> All()
    {
        var restaurantsList = await restaurants.All();

        return View(restaurantsList.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var restaurant = await restaurants.Details(id);

        restaurant.Data.Partner = (await users.GetPartners()).SingleOrDefault(u => u.Id == restaurant.Data.PartnerId);

        return View(restaurant.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        await restaurants.Approve(id);

        return RedirectToAction("All");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await restaurants.Delete(id);

        return RedirectToAction("All");
    }
}
