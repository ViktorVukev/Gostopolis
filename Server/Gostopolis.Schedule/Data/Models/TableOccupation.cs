namespace Gostopolis.Schedule.Data.Models;

public class TableOccupation
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int ReservationId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
