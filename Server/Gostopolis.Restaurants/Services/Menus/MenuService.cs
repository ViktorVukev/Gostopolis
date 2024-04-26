namespace Gostopolis.Restaurants.Services.Menus;

using Data;
using Data.Models;
using Gostopolis.Services;
using Models.Menus;

/// <summary>
/// Used to interact with menu records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
public class MenuService(
        RestaurantsDbContext db)
    : DataService<Menu>(db), IMenuService
{

    /// <inheritdoc />
    public async Task<int> Create(CreateMenuInputModel input)
    {
        var menu = new Menu()
        {
            Name = input.Name,
            RestaurantId = input.RestaurantId
        };

        await this.Save(menu);

        return menu.Id;
    }

    /// <inheritdoc />
    public async Task<bool> Update(EditMenuInputModel input)
    {
        var menu = this.All().SingleOrDefault(m => m.Id == input.Id);
        if (menu == null)
        {
            return false;
        }

        menu.Name = input.Name;

        await this.Data.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var menu = this.All().SingleOrDefault(m => m.Id == id);
        if (menu == null)
        {
            return false;
        }

        this.Data.Remove(menu);
        await this.Data.SaveChangesAsync();

        return true;
    }
}
