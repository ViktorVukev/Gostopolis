namespace Gostopolis.Statistics.Models;

using Data.Models;
using Gostopolis.Models;

public class StatisticsOutputModel : IMapFrom<Statistics>
{
    public int TotalRestaurants { get; set; }

    public int TotalAccommodations { get; set; }

    public int TotalReservations { get; set; }

    public int TotalPartners { get; set; }
}
