namespace Gostopolis.Restaurants.Models.Restaurants;

public class FilterRestaurantsInputModel
{
    //Search restaurants
    public int People { get; set; }

    public int Tables { get; set; }

    public string Town { get; set; }

    public DateTime StartTime { get; set; }

    //Filter restaurant
    public string Types { get; set; }

    public string Stars { get; set; }

    public bool HasParking { get; set; }

    public bool HasPosTerminal { get; set; }

    public bool AcceptPets { get; set; }
}
