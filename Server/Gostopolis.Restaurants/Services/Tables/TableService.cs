namespace Gostopolis.Restaurants.Services.Tables;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Gostopolis.Restaurants.Data.Models;
using Gostopolis.Services;
using Data;
using Models.Tables;

/// <summary>
/// Used to interact with table records.
/// </summary>
/// <param name="db">Current instance of the database.</param>
/// <param name="mapper">AutoMapper service instance.</param>
public class TableService(
        RestaurantsDbContext db,
        IMapper mapper)
    : DataService<Table>(db), ITableService
{
    /// <inheritdoc />
    public async Task<int> Create(CreateTableInputModel input)
    {
        var table = new Table()
        {
            Capacity = input.Capacity,
            Number = input.Number,
            IsSmokingAllowed = input.IsSmokingAllowed,
            IsOutdoor = input.IsOutdoor,
            RestaurantId = input.RestaurantId
        };

        await this.Save(table);

        return table.Id;
    }

    /// <inheritdoc />
    public async Task<TableDetailsOutputModel?> FindById(int id)
        => await mapper
            .ProjectTo<TableDetailsOutputModel>(this
                .All()
                .Where(t => t.Id == id))
            .FirstOrDefaultAsync();

    /// <inheritdoc />
    public IEnumerable<TableDetailsOutputModel> FindByReservationId(int id)
    {
        var reservationTablesIds = db.ReservationTables
            .Where(rt => rt.ReservationId == id)
            .Select(rt => rt.TableId)
            .ToList();

        return mapper.ProjectTo<TableDetailsOutputModel>(this
            .All()
            .Where(t => reservationTablesIds.Contains(t.Id)));
    }

    /// <inheritdoc />
    public async Task<bool> Update(EditTableInputModel input)
    {
        var table = await this.All().SingleOrDefaultAsync(t => t.Id == input.Id);
        if (table == null)
        {
            return false;
        }

        table.Capacity = input.Capacity;
        table.Number = input.Number;
        table.IsSmokingAllowed = input.IsSmokingAllowed;
        table.IsOutdoor = input.IsOutdoor;

        await this.Data.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var table = this.All().SingleOrDefault(t => t.Id == id);
        if (table == null)
        {
            return false;
        }

        this.Data.Remove(table);
        await this.Data.SaveChangesAsync();

        return true;
    }
}
