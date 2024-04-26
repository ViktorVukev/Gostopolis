namespace Gostopolis.Messages.Schedule;

public class TableOccupiedMessage
{
    public int TableId { get; set; }

    public int ReservationId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
