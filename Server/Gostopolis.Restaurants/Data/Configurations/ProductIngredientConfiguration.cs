namespace Gostopolis.Restaurants.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

/// <inheritdoc />
public class ProductIngredientConfiguration : IEntityTypeConfiguration<ProductIngredient>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ProductIngredient> builder)
    {
        builder
            .HasKey(pi => new { pi.ProductId, pi.IngredientId });

        builder
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductIngredients)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(pi => pi.Ingredient)
            .WithMany(i => i.ProductIngredients)
            .HasForeignKey(pi => pi.IngredientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}