namespace Gostopolis.Accommodations.Data.Models;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; }

    public EventType Type { get; set; }

    public string Description { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Location { get; set; }

    public string AccommodationId { get; set; }

    public Accommodation Accommodation { get; set; }
}
