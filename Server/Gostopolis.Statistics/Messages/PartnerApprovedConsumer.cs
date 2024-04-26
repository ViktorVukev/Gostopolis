namespace Gostopolis.Statistics.Messages;

using Gostopolis.Messages.Partners;
using MassTransit;
using Statistics;

public class PartnerApprovedConsumer(
        IStatisticsService statistics)
    : IConsumer<PartnerApprovedMessage>
{
    public async Task Consume(ConsumeContext<PartnerApprovedMessage> context)
        => await statistics.ApprovePartner();
}
