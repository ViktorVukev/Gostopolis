namespace Gostopolis.Restaurants.Services.RestaurantType;

using Gostopolis.Services;
using Data.Models;
using Models.RestaurantTypes;

/// <summary>
/// Used to interact with restaurant type records.
/// </summary>
public interface IRestaurantTypeService : IDataService<RestaurantType>
{
    /// <summary>
    /// Allows administrator to create restaurant types in the platform.
    /// </summary>
    /// <param name="input">Restaurant type's name and description.</param>
    /// <returns>The id of the newly created restaurant type.</returns>
    Task<int> Create(RestaurantTypeInputModel input);
    
    /// <summary>
    /// Gets all restaurant types in the platform.
    /// </summary>
    /// <returns>List of them.</returns>
    Task<IEnumerable<RestaurantTypeOutputModel>> GetAll();

    /// <summary>
    /// Updates restaurant type's information.
    /// </summary>
    /// <param name="id">Restaurant type's id.</param>
    /// <param name="input">Restaurant type's name and description.</param>
    /// <returns>Whether the restaurant type is successfully updated or not.</returns>
    Task<bool> Edit(int id, RestaurantTypeInputModel input);

    /// <summary>
    /// Deletes restaurant type.
    /// </summary>
    /// <param name="id">Restaurant type's id.</param>
    /// <returns>Whether the restaurant type is successfully deleted or not.</returns>
    Task<bool> Delete(int id);
}
