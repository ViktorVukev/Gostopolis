namespace Gostopolis.Restaurants.Data.Models;

public class RestaurantType
{
    public RestaurantType(string name, string description)
    {
        this.Name = name;
        this.Description = description;
    }

    public RestaurantType(int id, string name, string description)
        : this(name, description)
    {
        this.Id = id;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}