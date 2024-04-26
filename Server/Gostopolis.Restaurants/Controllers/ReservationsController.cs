namespace Gostopolis.Restaurants.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Gostopolis.Services;
using Models.Reservations;
using Services.Reservations;

using static Constants;

/// <summary>
/// Responds to user's requests related to the reservations.
/// </summary>
/// <param name="reservations">Service interface that keeps reservations business logic.</param>
public class ReservationsController(
        IReservationService reservations)
    : ApiController
{
    /// <summary>
    /// Creates a reservation in a restaurant.
    /// </summary>
    /// <param name="input">Contains reservations details.</param>
    /// <returns>The id of the newly created reservation.</returns>
    [HttpPost]
    [Authorize]
    public async Task<Result<int>> Create(
        CreateReservationInputModel input) 
        => await reservations.Create(input);

    /// <summary>
    /// Gets current user's reservations.
    /// </summary>
    /// <returns>A list of them.</returns>
    [HttpGet]
    [Route(nameof(Mine))]
    [Authorize]
    public Result<IEnumerable<ReservationDetailsOutputModel>> Mine()
        => Result<IEnumerable<ReservationDetailsOutputModel>>
            .SuccessWith(reservations.ByCurrentUser());

    /// <summary>
    /// Gets restaurant's reservations by given restaurant id.
    /// </summary>
    /// <param name="id">The id of the restaurant.</param>
    /// <returns>A list of them.</returns>
    [HttpGet]
    [Route(nameof(ByRestaurantId) + PathSeparator + Id)]
    [Authorize(Roles = PartnerRoleName)]
    public Result<IEnumerable<ReservationDetailsOutputModel>> ByRestaurantId(int id)
        => Result<IEnumerable<ReservationDetailsOutputModel>>
            .SuccessWith(reservations.ByRestaurantId(id));
}
