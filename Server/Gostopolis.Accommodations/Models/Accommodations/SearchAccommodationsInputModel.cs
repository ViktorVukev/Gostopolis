namespace Gostopolis.Accommodations.Models.Accommodations;

public class SearchAccommodationsInputModel
{
    public int People { get; set; }

    public int Rooms { get; set; }

    public string Town { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
