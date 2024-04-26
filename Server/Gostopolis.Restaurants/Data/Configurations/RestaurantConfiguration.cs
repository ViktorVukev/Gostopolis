namespace Gostopolis.Restaurants.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

/// <inheritdoc />
public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .HasOne(r => r.Type)
            .WithMany(t => t.Restaurants)
            .HasForeignKey(r => r.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(r => r.Images)
            .WithOne(i => i.Restaurant)
            .HasForeignKey(i => i.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(r => r.WorkingTime)
            .WithOne()
            .HasForeignKey<WorkingTime>(wt => wt.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.Tables)
            .WithOne(t => t.Restaurant)
            .HasForeignKey(t => t.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.Products)
            .WithOne(p => p.Restaurant)
            .HasForeignKey(p => p.RestaurantId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder
            .HasMany(r => r.Reservations)
            .WithOne(r => r.Restaurant)
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
