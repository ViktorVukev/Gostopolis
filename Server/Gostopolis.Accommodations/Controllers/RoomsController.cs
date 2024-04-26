namespace Gostopolis.Accommodations.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models.Rooms;
using Gostopolis.Controllers;
using Microsoft.AspNetCore.Authorization;
using Services.Rooms;

public class RoomsController(
        IRoomService rooms)
    : ApiController
{
    [HttpPost]
    [Authorize(Roles = "Partner")]
    public async Task<ActionResult<int>> Create(CreateRoomInputModel input)
        => await rooms.Create(input);

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<RoomDetailsOutputModel>> Details(int id)
        => await rooms.FindById(id);

    [HttpGet]
    [Route(nameof(ByAccommodationId))]
    public IEnumerable<RoomDetailsOutputModel> ByAccommodationId(int id)
        => rooms.ByAccommodationId(id);

    //[HttpPut]
    //[Route(Id)]
    //[Authorize(Roles = "Partner")]
    //public async Task<ActionResult> Edit(int id, EditUserInputModel input)
    //{
    //    var restaurant = await restaurants.FindById(id);

    //    if (!currentUser.IsAdministrator && currentUser.UserId != restaurant.PartnerId)
    //    {
    //        return BadRequest("You are not allowed to edit this property.");
    //    }

    //    return Ok(await restaurants.Edit(id, input));
    //}

    [HttpDelete]
    [Route(Id)]
    [Authorize(Roles = "Partner,Administrator")]
    public async Task<ActionResult> Delete(int id)
    {
        return Ok(await rooms.Delete(id));
    }
}
