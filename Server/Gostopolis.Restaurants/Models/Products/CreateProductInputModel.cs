namespace Gostopolis.Restaurants.Models.Products;

public class CreateProductInputModel
{
    public string Name { get; set; }

    public decimal Price { get; set; }

    public double Weight { get; set; }

    public int RestaurantId { get; set; }

    public int? MenuId { get; set; }

    public int Type { get; set; }

    public string? ImageBase64 { get; set; }

    public string? Ingredients { get; set; }
}
