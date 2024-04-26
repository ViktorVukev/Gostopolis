namespace Gostopolis.Accommodations.Data;

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Gostopolis.Data;
using Models;

public class AccommodationsDbContext : MessageDbContext
{
    public AccommodationsDbContext(DbContextOptions<AccommodationsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Accommodation> Accommodations { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<ReservationRoom> ReservationRooms { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<Meal> Meals { get; set; }

    public DbSet<AccommodationType> AccommodationTypes { get; set; }

    protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();
}
