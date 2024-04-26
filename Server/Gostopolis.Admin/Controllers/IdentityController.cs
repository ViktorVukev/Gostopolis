namespace Gostopolis.Admin.Controllers;

using AutoMapper;
using Gostopolis.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;
using Services.Identity;
using static Gostopolis.Infrastructure.InfrastructureConstants;

public class IdentityController(IIdentityService identityService,
    IMapper mapper) : AdministrationController
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn() => this.View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginFormModel model)
        => await this.Handle(
            async () =>
            {
                var result = await identityService
                    .Login(mapper.Map<UserInputModel>(model));

                this.Response.Cookies.Append(
                    AuthenticationCookieName,
                    result.Token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        MaxAge = TimeSpan.FromDays(1)
                    });
            },
            success: RedirectToAction(nameof(HomeController.Index), "Home"),
            failure: View("SignIn", model));

    [AuthorizeAdministrator]
    public IActionResult Logout()
    {
        this.Response.Cookies.Delete(AuthenticationCookieName);

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}