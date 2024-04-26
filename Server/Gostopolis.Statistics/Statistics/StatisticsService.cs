namespace Gostopolis.Statistics.Statistics;

using AutoMapper;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

public class StatisticsService : DataService<Statistics>, IStatisticsService
{
    private readonly IMapper mapper;

    public StatisticsService(StatisticsDbContext db, IMapper mapper)
        : base(db)
    {
        this.mapper = mapper;
    }

    public async Task<StatisticsOutputModel> Full()
        => await this.mapper
            .ProjectTo<StatisticsOutputModel>(this.All())
            .SingleOrDefaultAsync();

    public async Task AddRestaurant()
    {
        var statistics = await this.All().SingleOrDefaultAsync();

        statistics.TotalRestaurants++;

        await this.Data.SaveChangesAsync();
    }

    public async Task AddAccommodation()
    {
        var statistics = await this.All().SingleOrDefaultAsync();

        statistics.TotalAccommodations++;

        await this.Data.SaveChangesAsync();
    }

    public async Task ApprovePartner()
    {
        var statistics = await this.All().SingleOrDefaultAsync();

        statistics.TotalPartners++;

        await this.Data.SaveChangesAsync();
    }

    public async Task AddReservation()
    {
        var statistics = await this.All().SingleOrDefaultAsync();

        statistics.TotalReservations++;

        await this.Data.SaveChangesAsync();
    }
}