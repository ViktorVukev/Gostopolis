namespace Gostopolis.Restaurants.Services.Schedule;

using Refit;
using Gostopolis.Restaurants.Models.Restaurants;

/// <summary>
/// Used to interact with external schedule service.
/// </summary>
public interface IScheduleService
{
    /// <summary>
    /// Checks table availability for given period.
    /// </summary>
    /// <param name="input">Table's id and time period.</param>
    /// <returns>Whether a table is available for given period or not.</returns>
    [Get("/TableOccupations")]
    Task<bool> CheckAvailability(CheckAvailabilityInputModel input);
}
