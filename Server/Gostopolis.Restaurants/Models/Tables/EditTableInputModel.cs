namespace Gostopolis.Restaurants.Models.Tables;

public class EditTableInputModel
{
    public int Id { get; set; }

    public string Number { get; set; }

    public bool IsOutdoor { get; set; }

    public bool IsSmokingAllowed { get; set; }

    public int Capacity { get; set; }

    public int RestaurantId { get; set; }
}
