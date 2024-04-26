namespace Gostopolis.Restaurants.Data.Models;

public class Image
{
    public int Id { get; set; }

    public string Url { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }
}
