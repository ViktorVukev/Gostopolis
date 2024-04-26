namespace Gostopolis.Restaurants.Services.Menus;

using Gostopolis.Services;
using Data.Models;
using Models.Menus;

/// <summary>
/// Used to interact with menus records.
/// </summary>
public interface IMenuService : IDataService<Menu>
{
    /// <summary>
    /// Creates a menu in the database and assigns it to a restaurant by given id.
    /// </summary>
    /// <param name="input">Contains menu name and restaurant's id.</param>
    /// <returns>The id of the newly created menu.</returns>
    Task<int> Create(CreateMenuInputModel input);

    /// <summary>
    /// Updates menu's name.
    /// </summary>
    /// <param name="input">Contains menu's new name and restaurant's id.</param>
    /// <returns>Whether the menu is successfully updated or not.</returns>
    Task<bool> Update(EditMenuInputModel input);

    /// <summary>
    /// Deletes menu from the database.
    /// </summary>
    /// <param name="id">The id of the menu.</param>
    /// <returns>Whether the menu is successfully deleted or not.</returns>
    Task<bool> Delete(int id);
}
