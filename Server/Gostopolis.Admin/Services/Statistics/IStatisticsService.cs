namespace Gostopolis.Admin.Services.Statistics;

using Models.Statistics;
using Refit;

public interface IStatisticsService
{
    [Get("/Statistics")]
    Task<StatisticsOutputModel> Full();
}