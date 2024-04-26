namespace Gostopolis.Restaurants.Services.Reservations;

using Gostopolis.Services;
using Data.Models;
using Models.Reservations;

/// <summary>
/// Used to interact with reservation records.
/// </summary>
public interface IReservationService : IDataService<Reservation>
{
    /// <summary>
    /// Creates a reservation in the database and assigns it to a restaurant by given id.
    /// </summary>
    /// <param name="input">Contains reservation details.</param>
    /// <returns>The id of the newly created reservation.</returns>
    Task<int> Create(CreateReservationInputModel input);

    /// <summary>
    /// All reservations of the current user.
    /// </summary>
    /// <returns>List of the reservations.</returns>
    IEnumerable<ReservationDetailsOutputModel> ByCurrentUser();

    /// <summary>
    /// All reservations in the restaurant which id is given.
    /// </summary>
    /// <param name="id">The id of the restaurant.</param>
    /// <returns>List of the reservations.</returns>
    IEnumerable<ReservationDetailsOutputModel> ByRestaurantId(int id);
}
