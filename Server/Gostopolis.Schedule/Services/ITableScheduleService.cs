namespace Gostopolis.Schedule.Services;

using Gostopolis.Messages.Schedule;

public interface ITableScheduleService
{
    Task CreateReservation(TableOccupiedMessage message);

    Task RemoveReservation(int reservationId);

    Task<bool> IsAvailable(int tableId, DateTime startDate, DateTime endDate);
}
