namespace Gostopolis.Restaurants.Services.Products;

using Gostopolis.Services;
using Data.Models;
using Models.Products;

/// <summary>
/// Used to interact with product records.
/// </summary>
public interface IProductService : IDataService<Product>
{
    /// <summary>
    /// Creates a product in the database and assigns it to a restaurant by given id.
    /// </summary>
    /// <param name="input">Contains product details.</param>
    /// <returns>The id of the newly created product.</returns>
    Task<int> Create(CreateProductInputModel input);

    /// <summary>
    /// Updates product's details
    /// </summary>
    /// <param name="input">Contains product's new data and restaurant's id.</param>
    /// <returns>Whether the product is successfully updated or not.</returns>
    Task<bool> Update(EditProductInputModel input);

    /// <summary>
    /// Deletes product from the database
    /// </summary>
    /// <param name="id">The id of the product.</param>
    /// <returns>Whether the product is successfully deleted or not.</returns>
    Task<bool> Delete(int id);
}
