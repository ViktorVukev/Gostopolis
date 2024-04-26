namespace Gostopolis.Schedule.Models;

public class TableAvailabilityInputModel
{
    public int TableId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
