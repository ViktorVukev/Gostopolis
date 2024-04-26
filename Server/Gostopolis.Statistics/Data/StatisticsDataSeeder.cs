namespace Gostopolis.Statistics.Data;

using Models;
using Services;

public class StatisticsDataSeeder : IDataSeeder
{
    private readonly StatisticsDbContext db;

    public StatisticsDataSeeder(StatisticsDbContext db) => this.db = db;

    public void SeedData()
    {
        if (this.db.Statistics.Any())
        {
            return;
        }

        this.db.Statistics.Add(new Statistics
        {
            TotalAccommodations = 0,
            TotalRestaurants = 0,
            TotalPartners = 0,
            TotalReservations = 0
        });

        this.db.SaveChanges();
    }
}
