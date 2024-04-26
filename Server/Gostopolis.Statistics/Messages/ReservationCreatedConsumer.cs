namespace Gostopolis.Statistics.Messages;

using Gostopolis.Messages.Partners;
using MassTransit;
using Statistics;

public class ReservationCreatedConsumer(
        IStatisticsService statistics)
    : IConsumer<ReservationCreatedMessage>
{
    public async Task Consume(ConsumeContext<ReservationCreatedMessage> context)
        => await statistics.AddReservation();
}
