namespace Gostopolis.Accommodations.Data.Models;

public class Reservation
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public string ClientId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Guests { get; set; }

    public decimal TotalPrice { get; set; }

    public string? AdditionalInformation { get; set; }

    public int AccommodationId { get; set; }

    public Accommodation Accommodation { get; set; }
    
    public IList<ReservationRoom> ReservationRooms { get; set; } = new List<ReservationRoom>();
}
