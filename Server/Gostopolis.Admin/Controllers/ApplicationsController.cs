namespace Gostopolis.Admin.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models.Applications;
using Services.Identity;

public class ApplicationsController(
    IIdentityService users,
    IMapper mapper) : AdministrationController
{
    public async Task<IActionResult> All()
    {
        var applications = (await users.Applications())
            .OrderByDescending(a => a.CreatedOn)
            .ToList();

        foreach (var application in applications)
        {
            application.User = await users.Details(application.UserId);
        }

        return View(applications);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var application = await users.Details(id);

        application.User = await users.Details(application.UserId);

        return View(application);
    }

    [HttpPost]
    public async Task<IActionResult> Approve(int id)
        => await this.Handle(
            async () =>
            {
                var application = await users.Details(id);

                application.StatusInt = 2;
                application.Status = null;

                await users.Edit(id, mapper.Map<ApplicationDetailsOutputModel>(application));
            },
            success: RedirectToAction(nameof(All)),
            failure: RedirectToAction(nameof(All)));

    [HttpPost]
    public async Task<IActionResult> Decline(int id)
        => await this.Handle(
            async () =>
            {
                var application = await users.Details(id);

                application.StatusInt = 3;
                application.Status = null;

                await users.Edit(id, mapper.Map<ApplicationDetailsOutputModel>(application));
            },
            success: RedirectToAction(nameof(All)),
            failure: RedirectToAction(nameof(All)));
}