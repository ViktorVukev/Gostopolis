namespace Gostopolis.Accommodations.Data.Models;

public class Image
{
    public int Id { get; set; }

    public string Url { get; set; }

    public int AccommodationId { get; set; }

    public Accommodation Accommodation { get; set; }
}
