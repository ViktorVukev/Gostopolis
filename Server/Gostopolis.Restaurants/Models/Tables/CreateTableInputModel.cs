namespace Gostopolis.Restaurants.Models.Tables;

public class CreateTableInputModel
{
    public string Number { get; set; }

    public bool IsOutdoor { get; set; }

    public bool IsSmokingAllowed { get; set; }

    public int Capacity { get; set; }

    public int RestaurantId { get; set; }
}
