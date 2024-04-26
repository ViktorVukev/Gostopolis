namespace Gostopolis.Restaurants.Services.RestaurantType;

using Microsoft.EntityFrameworkCore;
using Gostopolis.Services;
using AutoMapper;
using Data;
using Data.Models;
using Models.RestaurantTypes;

/// <summary>
/// Used to interact with restaurant type records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
/// <param name="mapper">AutoMapper service instance.</param>
public class RestaurantTypeService(
        RestaurantsDbContext db,
        IMapper mapper)
    : DataService<RestaurantType>(db), IRestaurantTypeService
{
    /// <inheritdoc />
    public async Task<int> Create(RestaurantTypeInputModel input)
    {
        var restaurantType = new RestaurantType(input.Name, input.Description);

        await this.Save(restaurantType);

        return restaurantType.Id;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RestaurantTypeOutputModel>> GetAll()
        => await mapper
            .ProjectTo<RestaurantTypeOutputModel>(this.All())
            .ToListAsync();

    /// <inheritdoc />
    public async Task<bool> Edit(int id, RestaurantTypeInputModel input)
    {
        var restaurantType = await this.FindById(id);
        if (restaurantType == null)
        {
            return false;
        }

        restaurantType.Name = input.Name;
        restaurantType.Description = input.Description;

        await this.Save(restaurantType);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var restaurantType = await this.FindById(id);
        if (restaurantType == null)
        {
            return false;
        }

        this.Data.Remove(restaurantType);

        await this.Data.SaveChangesAsync();

        return true;
    }

    private async Task<RestaurantType?> FindById(int id)
        => await this.Data.FindAsync<RestaurantType>(id);
}
