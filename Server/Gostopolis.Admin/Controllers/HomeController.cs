namespace Gostopolis.Admin.Controllers;

using System.Diagnostics;
using Data;
using Gostopolis.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Identity;

public class HomeController(IIdentityService identity) : Controller
{
    public async Task<IActionResult> Index()
    {
        if (!this.User.IsAdministrator()) return RedirectToAction("SignIn", "Identity");

        var applications = await identity.Applications();
        return this.View(applications.Where(a => a.Status == ApplicationStatus.Pending));

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        => View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
        });
}