using Microsoft.AspNetCore.Mvc;

namespace Gostopolis.Admin.Controllers;

using Gostopolis.Services.Emails;
using Models.Users;
using Services.Identity;

public class UsersController(IIdentityService users, IEmailService email) : AdministrationController
{
    [HttpGet]
    public IActionResult Add() => this.View();

    [HttpPost]
    public async Task<IActionResult> Create(UserInputModel input)
    {
        await email.Send(
            "Auto-generated password",
            $"Use the password below in order to log into your account \n\n\n {input.Password}", 
            input.Email);

        await users.Register(input);

        return RedirectToAction("All");
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        return View(await users.GetAll());
    }

    [HttpGet]
    public async Task<IActionResult> Partners()
    {
        return View(await users.GetPartners());
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await users.Details(id);

        ViewBag.Id = id;
        
        return this.View(new EditUserInputModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, EditUserInputModel model)
    {
        await users.EditUser(id, model);

        return this.RedirectToAction("All");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await users.Delete(id);

        return this.RedirectToAction("All");
    }
}
