namespace Gostopolis.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models.Accommodations;
using Models.Restaurants;
using Services.Accommodations;

public class AccommodationTypesController(
    IAccommodationService accommodations) 
    : AdministrationController
{
    [HttpGet]
    public async Task<IActionResult> Add() => this.View();

    [HttpPost]
    public async Task<IActionResult> Create(AccommodationTypeInputModel model)
    {
        await accommodations.CreateAccommodationType(model);

        return this.RedirectToAction("All");
    }

    public async Task<IActionResult> All()
        => View(await accommodations.AccommodationTypes());

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var accommodationType = (await accommodations.AccommodationTypes())
            .FirstOrDefault(at => at.Id == id);

        ViewBag.Id = id;

        return this.View(new AccommodationTypeInputModel()
        {
            Name = accommodationType.Name,
            Description = accommodationType.Description
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, AccommodationTypeInputModel model)
    {
        await accommodations.EditAccommodationType(id, model);

        return this.RedirectToAction("All");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await accommodations.DeleteAccommodationType(id);

        return this.RedirectToAction("All");
    }
}
