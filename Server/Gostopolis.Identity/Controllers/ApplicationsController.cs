namespace Gostopolis.Identity.Controllers;

using Data.Models;
using Gostopolis.Controllers;
using Gostopolis.Data;
using Gostopolis.Infrastructure;
using Gostopolis.Services.Emails;
using Gostopolis.Services.Files;
using Gostopolis.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Applications;
using Services.Applications;
using Services.Users;

/// <summary>
/// Create, edit and delete user applications for becoming partners.
/// </summary>
/// <param name="currentUser"></param>
/// <param name="files"></param>
/// <param name="applications"></param>
[Authorize(AuthenticationSchemes = "Bearer")]
public class ApplicationsController(
        ICurrentUserService currentUser,
        IUserService users,
        IFileService files,
        IEmailService emails,
        IApplicationService applications) 
    : ApiController
{
    [HttpGet]
    [Route("HasApplied")]
    public async Task<ActionResult<bool>> HasApplied()
        => (await applications.GetAll()).Any(application => application.UserId == currentUser.UserId);

    [HttpGet]
    [Route(nameof(HasPendingApplication))]
    public async Task<ActionResult<bool>> HasPendingApplication()
        => (await applications.GetAll()).Any(application => application.UserId == currentUser.UserId && application.Status == ApplicationStatus.Pending);

    [HttpGet]
    [Route("Mine")]
    public async Task<IEnumerable<ApplicationDetailsOutputModel>> Mine()
        => (await applications.GetAll())
            .Where(application => application.UserId == currentUser.UserId)
            .OrderByDescending(application => application.CreatedOn);

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateApplicationInputModel input)
    {
        var userApplications = await applications.FindByUser(currentUser.UserId);
        if (userApplications.Any(a => a.Status is ApplicationStatus.Pending or ApplicationStatus.Approved))
        {
            if (userApplications.Count(a => a.Status == ApplicationStatus.Declined) > 5)
            {
                return BadRequest("This user can no longer be a partner.");
            }

            return BadRequest("The user can only have one pending or approved application.");
        }

        var documentUrl = await files.UploadFileAsync(input.DocumentBase64);

        var application = new Application()
        {
            UserId = currentUser.UserId,
            DocumentUrl = documentUrl
        };

        await applications.Save(application);

        return application.Id;
    }

    [HttpGet]
    [AuthorizeAdministrator]
    public async Task<IEnumerable<ApplicationDetailsOutputModel>> All()
        => await applications.GetAll();

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<ApplicationDetailsOutputModel>> Details(int id)
        => await applications.GetDetails(id);

    [HttpPut]
    [Route(Id)]
    [AuthorizeAdministrator]
    public async Task<ActionResult> Edit(int id, ApplicationDetailsOutputModel input)
    {
        var application = await applications.FindById(id);

        if (application.Status != ApplicationStatus.Pending)    
        {
            return BadRequest("You can modify application only once.");
        }

        if (input.StatusInt != null)
        {
            application.Status = (ApplicationStatus)input.StatusInt;
        }

        switch (application.Status)
        {
            case ApplicationStatus.Approved:
                application.ApprovedOn = DateTime.UtcNow;
                await users.BecomePartner(application.UserId);
                await emails.Send(
                    "Partner approval",
                    "Your application for becoming our partner has been approved. You are now able to list your properties in our platform.",
                    (await users.FindById(application.UserId)).Email);
                break;
            case ApplicationStatus.Declined:
                application.DeclinedOn = DateTime.UtcNow;
                await emails.Send(
                    "Partner application",
                    "We are sorry to inform you, that your application for becoming our partner has been declined. Make sure that your names and profile picture are matching these on the identification document.",
                    (await users.FindById(application.UserId)).Email);
                break;
        }

        await applications.Save(application);

        return Ok();
    }
}
