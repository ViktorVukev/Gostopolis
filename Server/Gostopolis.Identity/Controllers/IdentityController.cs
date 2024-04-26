namespace Gostopolis.Identity.Controllers;

using Gostopolis.Controllers;
using Gostopolis.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Identity;
using Services.Identity;

public class IdentityController(
        IIdentityService identity,
        ICurrentUserService currentUser)
    : ApiController
{
    /// <summary>
    /// Creates a new user in the application
    /// </summary>
    /// <param name="input">Names, email, password and phone number which are required for creating a user.</param>
    /// <returns>An object that contains the newly created user's id.</returns>
    /// <response code="200">If the user is successfully created.</response>
    /// <response code="400">If any errors occurred during register process.</response>
    [HttpPost]
    [Route(nameof(Register))]
    public async Task<ActionResult<UserOutputModel>> Register(UserInputModel input)
    {
        var result = await identity.Register(input);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return new UserOutputModel(result.Data.Id);
    }

    [HttpPost]
    [Route(nameof(ResendVerification))]
    public async Task<ActionResult<UserOutputModel>> ResendVerification(string userId)
    {
        var result = await identity.EmailVerification(userId);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost]
    [Route(nameof(ConfirmEmail))]
    public async Task<ActionResult<bool>> ConfirmEmail(UserOutputModel input)
    {
        var result = await identity.ConfirmEmail(input);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    [Route(nameof(ResetEmail))]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult> ResetEmail(ResetEmailInputModel input)
        => await identity.ResetEmail(input);

    [HttpPut]
    [Route(nameof(ChangeEmail))]
    public async Task<ActionResult> ChangeEmail(ChangeEmailInputModel input)
        => await identity.ChangeEmail(input);

    [HttpPost]
    [Route(nameof(Login))]
    public async Task<ActionResult<UserOutputModel>> Login(UserInputModel input)
    {
        var result = await identity.Login(input);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return new UserOutputModel(result.Data.Token, result.Data.UserId);
    }

    [HttpPost]
    [Route(nameof(ResetPassword))]
    public async Task<ActionResult> ResetPassword(ResetPasswordInputModel input)
        => await identity.ResetPassword(input);

    [HttpPut]
    [Route(nameof(ChangePassword))]
    public async Task<ActionResult> ChangePassword(ChangePasswordInputModel input)
        =>await identity.ChangePassword(input);
}