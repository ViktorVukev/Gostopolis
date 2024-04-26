namespace Gostopolis.Schedule.Data;

using Models;
using Gostopolis.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public class ScheduleDbContext : MessageDbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options)
    {
    }

    public DbSet<RoomOccupation> RoomOccupations { get; set; }

    public DbSet<TableOccupation> TableOccupations { get; set; }

    protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
}
