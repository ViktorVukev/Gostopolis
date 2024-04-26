namespace Gostopolis.Restaurants.Services.Products;

using Microsoft.EntityFrameworkCore;
using Gostopolis.Services;
using Gostopolis.Services.Files;
using Data;
using Data.Models;
using Ingredients;
using Models.Products;

/// <summary>
/// Used to interact with product records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
/// <param name="fileService">Common interface for uploading images.</param>
/// <param name="ingredients">Service interface for the ingredients.</param>
public class ProductService(
        RestaurantsDbContext db,
        IFileService fileService,
        IIngredientService ingredients)
    : DataService<Product>(db), IProductService
{
    /// <inheritdoc />
    public async Task<int> Create(CreateProductInputModel input)
    {
        string? imageUrl = null;

        if (input.ImageBase64 != null)
        {
            imageUrl = await fileService.UploadFileAsync(input.ImageBase64);
        }

        var product = new Product()
        {
            Name = input.Name,
            Price = input.Price,
            Weight = input.Weight,
            RestaurantId = input.RestaurantId,
            MenuId = input.MenuId,
            Type = (ProductType)input.Type,
            ImageUrl = imageUrl
        };

        await this.Save(product);

        await db.SaveChangesAsync();

        if (input.Ingredients != null)
        {
            await ingredients.AddNonExistingIngredients(input.Ingredients);

            await ingredients.AddIngredientsToProduct(input.Ingredients, product.Id);
        }

        return product.Id;
    }

    /// <inheritdoc />
    public async Task<bool> Update(EditProductInputModel input)
    {
        var product = await this.All().SingleOrDefaultAsync(p => p.Id == input.Id);

        if (product == null)
        {
            return false;
        }

        product.Name = input.Name;
        product.Price = input.Price;
        product.Weight = input.Weight;
        product.MenuId = input.MenuId;
        product.Type = (ProductType)input.Type;
        
        if (input.ImageBase64 != null)
        {
            product.ImageUrl = await fileService.UploadFileAsync(input.ImageBase64);
        }

        if (input.Ingredients != null)
        {
            await ingredients.AddNonExistingIngredients(input.Ingredients);

            //TODO: Delete product's ingredients and apply the new ones
        }

        await db.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var product = await this.All().SingleOrDefaultAsync(p => p.Id == id);
        if (product == null)
        {
            return false;
        }

        this.Data.Remove(product);
        await this.Data.SaveChangesAsync();

        return true;
    }
}
