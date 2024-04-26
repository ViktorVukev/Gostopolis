namespace Gostopolis.Accommodations.Controllers;

using Gostopolis.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models.Reservations;
using Services.Reservations;

public class ReservationsController(
        IReservationService reservations)
    : ApiController
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> Create(CreateReservationInputModel input)
        => await reservations.Create(input);

    [HttpGet]
    [Route("Mine")]
    [Authorize]
    public IEnumerable<ReservationDetailsOutputModel> Mine()
        => reservations.ByCurrentUser();

    [HttpGet]
    [Route("ByAccommodationId" + PathSeparator + Id)]
    [Authorize(Roles = "Partner")]
    public IEnumerable<ReservationDetailsOutputModel> ByAccommodationId(int id)
        => reservations.ByAccommodationId(id);
}
