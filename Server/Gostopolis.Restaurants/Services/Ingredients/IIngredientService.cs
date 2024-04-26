namespace Gostopolis.Restaurants.Services.Ingredients;

using Gostopolis.Services;
using Data.Models;


/// <summary>
/// Used to interact with ingredients records.
/// </summary>
public interface IIngredientService : IDataService<Ingredient>
{
    /// <summary>
    /// Gets all ingredients from the database.
    /// </summary>
    /// <returns>A list of all ingredients.</returns>
    IEnumerable<string> GetAll();

    /// <summary>
    /// Adds to the database all product ingredients that are entered for the first time.
    /// </summary>
    /// <param name="ingredients">Text that contains product's ingredients.</param>
    /// <returns>Whether they are successfully stored in the database or not.</returns>
    Task<bool> AddNonExistingIngredients(string ingredients);

    /// <summary>
    /// Gets a list of ingredients and adds each of them to the given product.
    /// </summary>
    /// <param name="ingredients">Text that contains product's ingredients.</param>
    /// <param name="productId">The product's id.</param>
    /// <returns></returns>
    Task<bool> AddIngredientsToProduct(string ingredients, int productId);
}
