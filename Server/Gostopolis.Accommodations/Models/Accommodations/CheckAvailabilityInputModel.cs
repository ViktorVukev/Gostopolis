namespace Gostopolis.Accommodations.Models.Accommodations;

public class CheckAvailabilityInputModel
{
    public int RoomId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
