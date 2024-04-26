namespace Gostopolis.Statistics.Messages;

using MassTransit;
using Gostopolis.Messages.Partners;
using Statistics;

public class RestaurantCreatedConsumer(
    IStatisticsService statistics) 
    : IConsumer<RestaurantCreatedMessage>
{
    public async Task Consume(ConsumeContext<RestaurantCreatedMessage> context)
        => await statistics.AddRestaurant();
}
