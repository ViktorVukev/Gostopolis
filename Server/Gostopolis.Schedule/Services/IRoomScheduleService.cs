namespace Gostopolis.Schedule.Services;

using Gostopolis.Messages.Schedule;

public interface IRoomScheduleService
{
    Task CreateReservation(RoomOccupiedMessage message);

    Task RemoveReservation(int reservationId);

    Task<bool> IsAvailable(int roomId, DateTime startDate, DateTime endDate);
}
