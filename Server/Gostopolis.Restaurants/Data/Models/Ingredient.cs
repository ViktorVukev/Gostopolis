namespace Gostopolis.Restaurants.Data.Models;

public class Ingredient(string name)
{
    public int Id { get; set; }

    public string Name { get; set; } = name;

    public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
}