namespace Gostopolis.Accommodations.Services.Reservations;

using Models.Reservations;

public interface IReservationService
{
    Task<int> Create(CreateReservationInputModel input);

    IEnumerable<ReservationDetailsOutputModel> ByCurrentUser();

    IEnumerable<ReservationDetailsOutputModel> ByAccommodationId(int id);
}
