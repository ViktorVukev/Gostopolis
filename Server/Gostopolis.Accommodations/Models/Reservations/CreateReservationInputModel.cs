namespace Gostopolis.Accommodations.Models.Reservations;

public class CreateReservationInputModel
{
    public DateOnly DateOfBirth { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string ClientEmail { get; set; }

    public string PartnerEmail { get; set; }

    public int Guests { get; set; }

    public int Nights { get; set; }

    public string? AdditionalInformation { get; set; }

    public string RoomIds { get; set; }

    public int AccommodationId { get; set; }
}
