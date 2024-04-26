namespace Gostopolis.Identity.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Infrastructure;
using Gostopolis.Services.Identity;
using Models.Partners;
using Services.Users;

public class UsersController(
        IUserService users, 
        ICurrentUserService currentUser) 
    : ApiController
{
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("IsPartner")]
    public async Task<ActionResult<bool>> IsPartner()
    {
        var userId = currentUser.UserId;

        return await users.IsPartner(userId);
    }

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<UserDetailsOutputModel>> Details(string id)
        => await users.GetDetails(id);

    [HttpGet]
    [Route("Current")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult<UserDetailsOutputModel>> ByCurrentUserId()
        => await users.GetDetails(currentUser.UserId);

    [HttpGet]
    [AuthorizeAdministrator(AuthenticationSchemes = "Bearer")]
    public async Task<IEnumerable<UserDetailsOutputModel>> All()
        => await users.GetAll();

    [HttpGet]
    [Route("Partners")]
    [AuthorizeAdministrator(AuthenticationSchemes = "Bearer")]
    public async Task<IEnumerable<UserDetailsOutputModel>> Partners()
        => await users.GetPartners();

    [HttpPut]
    [Route(Id)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult> Edit(string id, EditUserInputModel input)
    {
        if (!currentUser.IsAdministrator && currentUser.UserId != id)
        {
            return BadRequest("You are not allowed to edit this user.");
        }

        return Ok(await users.Edit(id, input));
    }

    [HttpPut]
    [Route("Image")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult> EditImage(EditUserImageInputModel input)
        => Ok(await users.EditUserImage(input));

    [HttpPut]
    [Route("EmailPreferences")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult> EditEmailPreferences(EditUserEmailPreferencesInputModel input) 
        => Ok(await users.EditEmailPreferences(input));

    [HttpDelete]
    [Route(Id)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<ActionResult> Delete(string id)
    {
        if (!currentUser.IsAdministrator && currentUser.UserId != id)
        {
            return BadRequest("You are not allowed to edit this user.");
        }

        return Ok(await users.Delete(id));
    }
}