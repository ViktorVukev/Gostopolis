namespace Gostopolis.Restaurants.Data;

using System.Reflection;
using Gostopolis.Data;
using Microsoft.EntityFrameworkCore;
using Models;

/// <inheritdoc />
public class RestaurantsDbContext : MessageDbContext
{
    /// <inheritdoc />
    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }

    public DbSet<RestaurantType> RestaurantTypes { get; set; }

    public DbSet<WorkingTime> WorkingTimes { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<Table> Tables { get; set; }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<ProductIngredient> ProductIngredients { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    //public DbSet<Order> Orders { get; set; }

    //public DbSet<OrderProduct> OrderProducts { get; set; }

    public DbSet<Reservation> Reservations { get; set; }

    public DbSet<ReservationTable> ReservationTables { get; set; }

    //public DbSet<Review> Reviews { get; set; }

    /// <inheritdoc />
    protected override Assembly ConfigurationsAssembly => Assembly.GetExecutingAssembly();

}