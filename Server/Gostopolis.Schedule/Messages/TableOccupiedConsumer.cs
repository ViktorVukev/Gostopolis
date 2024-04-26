using Gostopolis.Messages.Schedule;
using Gostopolis.Schedule.Services;
using MassTransit;

namespace Gostopolis.Schedule.Messages;

public class TableOccupiedConsumer(
        ITableScheduleService tableSchedule)
    : IConsumer<TableOccupiedMessage>
{
    public async Task Consume(ConsumeContext<TableOccupiedMessage> context)
        => await tableSchedule.CreateReservation(context.Message);
}
