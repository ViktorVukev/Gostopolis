namespace Gostopolis.Accommodations.Services.Schedule;

using Models.Accommodations;
using Refit;

public interface IScheduleService
{
    [Get("/RoomOccupations")]
    Task<bool> CheckAvailability(CheckAvailabilityInputModel input);
}
