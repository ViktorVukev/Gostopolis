namespace Gostopolis.Statistics.Data;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Gostopolis.Data;
using Models;

public class StatisticsDbContext : MessageDbContext
{
    public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Statistics> Statistics { get; set; }

    protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
}
