namespace Gostopolis.Accommodations.Models.Accommodations;

public class FilterAccommodationsInputModel
{
    //Search accommodations
    public int People { get; set; }

    public int Rooms { get; set; }

    public string Town { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    //Filter accommodations
    public string Types { get; set; }

    public string Stars { get; set; }

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public bool HasParking { get; set; }

    public bool HasPosTerminal { get; set; }

    public bool AcceptPets { get; set; }

    public string Facilities { get; set; }
}
