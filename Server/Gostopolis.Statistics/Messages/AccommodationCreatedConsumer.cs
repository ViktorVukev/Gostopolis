namespace Gostopolis.Statistics.Messages;

using Gostopolis.Messages.Partners;
using MassTransit;
using Statistics;

public class AccommodationCreatedConsumer(
        IStatisticsService statistics)
    : IConsumer<AccommodationCreatedMessage>
{
    public async Task Consume(ConsumeContext<AccommodationCreatedMessage> context)
        => await statistics.AddAccommodation();
}
