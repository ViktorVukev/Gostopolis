namespace Gostopolis.Restaurants.Data.Models;

public class Review
{
    public int Id { get; set; }

    public bool IsRecommended { get; set; }

    public string Content { get; set; }

    public int UserId { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }
}