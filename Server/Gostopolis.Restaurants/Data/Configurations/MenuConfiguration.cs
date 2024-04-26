namespace Gostopolis.Restaurants.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

/// <inheritdoc />
public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .HasOne(m => m.Restaurant)
            .WithMany(r => r.Menus)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(r => r.Products)
            .WithOne(p => p.Menu)
            .HasForeignKey(p => p.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}