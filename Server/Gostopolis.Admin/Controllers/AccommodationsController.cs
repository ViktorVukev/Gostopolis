using Microsoft.AspNetCore.Mvc;

namespace Gostopolis.Admin.Controllers;

using AutoMapper;
using Services.Accommodations;
using Services.Identity;

public class AccommodationsController(
    IAccommodationService accommodations,
    IIdentityService users,
    IMapper mapper) : AdministrationController
{
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var accommodationsList = await accommodations.All();

        return View(accommodationsList);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var accommodation = await accommodations.Details(id);

        accommodation.Partner = (await users.GetPartners()).SingleOrDefault(u => u.Id == accommodation.PartnerId);

        return View(accommodation);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        await accommodations.Approve(id);

        return RedirectToAction("All");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await accommodations.Delete(id);

        return RedirectToAction("All");
    }
}
