namespace Gostopolis.Statistics.Controllers;

using Microsoft.AspNetCore.Mvc;
using Gostopolis.Controllers;
using Statistics;
using Models;

public class StatisticsController(
    IStatisticsService statistics) 
    : ApiController
{
    [HttpGet]
    public async Task<StatisticsOutputModel> Full()
        => await statistics.Full();
}
