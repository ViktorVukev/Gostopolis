namespace Gostopolis.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models.RestaurantTypes;
using Services.Restaurants;

public class RestaurantTypesController(
    IRestaurantService restaurants) 
    : AdministrationController
{
    [HttpGet]
    public async Task<IActionResult> Add() => this.View();

    [HttpPost]
    public async Task<IActionResult> Create(RestaurantTypeInputModel model)
    {
        await restaurants.CreateRestaurantType(model);

        return this.RedirectToAction("All");
    }

    public async Task<IActionResult> All()
        => View((await restaurants.RestaurantTypes()).Data);

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var restaurantType = (await restaurants.RestaurantTypes())
            .Data
            .FirstOrDefault(at => at.Id == id);

        ViewBag.Id = id;

        return this.View(new RestaurantTypeInputModel()
        {
            Name = restaurantType.Name,
            Description = restaurantType.Description
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, RestaurantTypeInputModel model)
    {
        await restaurants.EditRestaurantType(id, model);

        return this.RedirectToAction("All");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await restaurants.DeleteRestaurantType(id);

        return this.RedirectToAction("All");
    }
}
