namespace Gostopolis.Restaurants.Services.Tables;

using Gostopolis.Restaurants.Data.Models;
using Gostopolis.Services;
using Models.Tables;

/// <summary>
/// Used to interact with table records.
/// </summary>
public interface ITableService : IDataService<Table>
{
    /// <summary>
    /// Creates a table in the database and assigns it to a restaurant by given id.
    /// </summary>
    /// <param name="input">Contains table name and restaurant's id.</param>
    /// <returns>The id of the newly created table.</returns>
    Task<int> Create(CreateTableInputModel input);

    /// <summary>
    /// Gets table by given id.
    /// </summary>
    /// <param name="id">The id of the table.</param>
    /// <returns>The details of the table.</returns>
    Task<TableDetailsOutputModel?> FindById(int id);

    /// <summary>
    /// Gets reservation's tables.
    /// </summary>
    /// <param name="id">The id of the reservations.</param>
    /// <returns>The details of the tables that are reserved for the reservation.</returns>
    IEnumerable<TableDetailsOutputModel> FindByReservationId(int id);

    /// <summary>
    /// Updates table's details.
    /// </summary>
    /// <param name="input">Contains table's new details.</param>
    /// <returns>Whether the table is successfully updated or not.</returns>
    Task<bool> Update(EditTableInputModel input);

    /// <summary>
    /// Deletes table from the database.
    /// </summary>
    /// <param name="id">The id of the table.</param>
    /// <returns>Whether the table is successfully deleted or not.</returns>
    Task<bool> Delete(int id);
}
