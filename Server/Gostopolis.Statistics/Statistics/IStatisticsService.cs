namespace Gostopolis.Statistics.Statistics;

using Models;

public interface IStatisticsService
{
    Task<StatisticsOutputModel> Full();

    Task AddRestaurant();

    Task AddAccommodation();

    Task ApprovePartner();

    Task AddReservation();
}
