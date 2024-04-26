namespace Gostopolis.Schedule.Controllers;

using Gostopolis.Controllers;
using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

public class TableOccupationsController(ITableScheduleService tableSchedule) : ApiController
{
    [HttpGet]
    public async Task<ActionResult<bool>> IsAvailable([FromQuery] TableAvailabilityInputModel input)
        => await tableSchedule.IsAvailable(input.TableId, input.StartDate, input.EndDate);
}
