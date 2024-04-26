namespace Gostopolis.Accommodations.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Models.Accommodations;
using Services.Accommodations;
using Infrastructure;
using Gostopolis.Services.Identity;

public class AccommodationsController(
        ICurrentUserService currentUser,
        IAccommodationService accommodations)
    : ApiController
{
    [HttpPost]
    [Authorize(Roles = "Partner")]
    public async Task<ActionResult<int>> Create(CreateAccommodationInputModel input)
        => await accommodations.Create(input);

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<AccommodationDetailsOutputModel>> Details(int id)
        => await accommodations.FindById(id);

    [HttpGet]
    [Route(Id + PathSeparator + "Offers")]
    public async Task<ActionResult<AccommodationDetailsOutputModel>> Details(int id, [FromQuery] SearchAccommodationsInputModel input)
        => await accommodations.FindById(id, input);

    [HttpGet]
    [Route("All")]
    [AuthorizeAdministrator]
    public IEnumerable<AccommodationDetailsOutputModel> All()
        => accommodations.GetAll();

    [HttpGet]
    public IEnumerable<AccommodationDetailsOutputModel> Listing(
        [FromQuery] SearchAccommodationsInputModel model)
        => accommodations.GetListing(model);

    [HttpGet]
    [Route(nameof(Filter))]
    public IEnumerable<AccommodationDetailsOutputModel> Filter(
        [FromQuery] FilterAccommodationsInputModel model)
        => accommodations.Filter(model);

    [HttpGet]
    [Route("Mine")]
    [Authorize(Roles = "Partner,Administrator")]
    public IEnumerable<AccommodationDetailsOutputModel> Mine()
        => accommodations.ByCurrentUser();

    [HttpPut]
    [Route(Id)]
    [Authorize(Roles = "Partner")]
    public async Task<ActionResult> Edit(int id, EditAccommodationInputModel input)
    {
        var accommodation = await accommodations.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != accommodation.PartnerId)
        {
            return BadRequest("Нямате право да редактирате този обект.");
        }

        return Ok(await accommodations.Edit(id, input));
    }

    [HttpPut]
    [Route("Approve" + PathSeparator + Id)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Approve(int id)
    {
        var accommodation = await accommodations.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != accommodation.PartnerId)
        {
            return BadRequest("Нямате право да редактирате този обект.");
        }

        return Ok(await accommodations.Approve(id));
    }

    [HttpPut]
    [Route("ChangeVisibility" + PathSeparator + Id)]
    [Authorize(Roles = "Partner,Administrator")]
    public async Task<ActionResult> ChangeVisibility(int id)
    {
        var accommodation = await accommodations.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != accommodation.PartnerId)
        {
            return BadRequest("You are not allowed to edit this property.");
        }

        return Ok(await accommodations.ChangeVisibility(id));
    }

    [HttpDelete]
    [Route(Id)]
    [Authorize(Roles = "Partner,Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        var restaurant = await accommodations.FindById(id);

        if (!currentUser.IsAdministrator && currentUser.UserId != restaurant.PartnerId)
        {
            return BadRequest("Нямате право да редактирате този обект.");
        }

        return Ok(await accommodations.Delete(id));
    }
}
