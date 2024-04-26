namespace Gostopolis.Restaurants.Models.Restaurants;

public class SearchRestaurantsInputModel
{
    public int People { get; set; }

    public int Tables { get; set; }

    public string Town { get; set; }

    public DateTime StartTime { get; set; }
}
