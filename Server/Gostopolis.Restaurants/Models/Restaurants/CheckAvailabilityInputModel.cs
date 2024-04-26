namespace Gostopolis.Restaurants.Models.Restaurants;

public class CheckAvailabilityInputModel
{
    public int TableId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
