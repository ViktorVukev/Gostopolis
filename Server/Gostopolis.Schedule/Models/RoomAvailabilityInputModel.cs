namespace Gostopolis.Schedule.Models;

public class RoomAvailabilityInputModel
{
    public int RoomId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
