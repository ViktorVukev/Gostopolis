namespace Gostopolis.Messages.Schedule;
public class RoomOccupiedMessage
{
    public int RoomId { get; set; }

    public int ReservationId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
