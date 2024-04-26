namespace Gostopolis.Schedule.Data.Models;

public class RoomOccupation
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int ReservationId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
