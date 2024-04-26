namespace Gostopolis.Restaurants.Data.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? ImageUrl { get; set; }

    public double Weight { get; set; }

    public decimal Price { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }

    public int? MenuId { get; set; }

    public Menu? Menu { get; set; }

    public ProductType Type { get; set; }

    public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();

    //public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}