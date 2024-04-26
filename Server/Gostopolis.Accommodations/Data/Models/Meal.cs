namespace Gostopolis.Accommodations.Data.Models;

public class Meal
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int AccommodationId { get; set; }

    public Accommodation Accommodation { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
