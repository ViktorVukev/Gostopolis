namespace Gostopolis.Restaurants.Services.Ingredients;

using Gostopolis.Restaurants.Data.Models;
using Gostopolis.Services;
using Data;

/// <summary>
/// Used to interact with ingredients records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
public class IngredientService(
        RestaurantsDbContext db)
    : DataService<Ingredient>(db), IIngredientService
{
    /// <inheritdoc />
    public IEnumerable<string> GetAll()
        => this
            .All()
            .Select(i => i.Name)
            .ToList();

    /// <inheritdoc />
    public async Task<bool> AddNonExistingIngredients(string ingredients)
    {
        ingredients
            .Split(", ")
            .Select(i => i.Trim().ToLower())
            .Where(pi => !db.Ingredients.Any(i => i.Name == pi))
            .ToList()
            .ForEach(async i => await db.AddAsync(new Ingredient(i)));

        await db.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddIngredientsToProduct(string ingredients, int productId)
    {
        db.Ingredients
            .Where(i => ingredients.Contains(i.Name))
            .ToList()
            .ForEach(async ingredient => await db.AddAsync(new ProductIngredient()
            {
                IngredientId = ingredient.Id,
                ProductId = productId
            }));

        await db.SaveChangesAsync();

        return true;
    }
}
