namespace Gostopolis.Restaurants.Models.Menus;

public class EditMenuInputModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int RestaurantId { get; set; }
}