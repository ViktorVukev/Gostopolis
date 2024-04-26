namespace Gostopolis.Schedule.Messages;

using Gostopolis.Messages.Partners;
using Gostopolis.Messages.Schedule;
using MassTransit;
using Services;

public class RoomOccupiedConsumer(
        IRoomScheduleService roomSchedule)
    : IConsumer<RoomOccupiedMessage>
{
    public async Task Consume(ConsumeContext<RoomOccupiedMessage> context)
        => await roomSchedule.CreateReservation(context.Message);
}
