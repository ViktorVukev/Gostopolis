namespace Gostopolis.Schedule.Controllers;

using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Models;
using Services;

public class RoomOccupationsController(IRoomScheduleService roomSchedule) : ApiController
{
    [HttpGet]
    public async Task<ActionResult<bool>> IsAvailable([FromQuery] RoomAvailabilityInputModel input)
        => await roomSchedule.IsAvailable(input.RoomId, input.StartDate, input.EndDate);
}
