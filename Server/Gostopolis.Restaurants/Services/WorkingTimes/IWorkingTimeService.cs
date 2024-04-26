namespace Gostopolis.Restaurants.Services.WorkingTimes;

using Models.WorkingTimes;

/// <summary>
/// Used to interact with working time records.
/// </summary>
public interface IWorkingTimeService
{
    /// <summary>
    /// Creates a working time in the database and assigns it to a restaurant by given id.
    /// </summary>
    /// <param name="input">Contains working hours.</param>
    /// <returns>The id of the newly created working time.</returns>
    Task<int> Create(WorkingTimeInputModel input);

    /// <summary>
    /// Updates restaurant's working time
    /// </summary>
    /// <param name="input">Contains new working hours.</param>
    /// <returns>Whether the working time is successfully updated or not.</returns>
    Task<bool> Edit(WorkingTimeInputModel input);

    /// <summary>
    /// Deletes working time from the database
    /// </summary>
    /// <param name="id">The id of the working time.</param>
    /// <returns>Whether the working time is successfully deleted or not.</returns>
    Task<bool> Delete(int id);
}
