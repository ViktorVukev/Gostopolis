using Gostopolis.Accommodations.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Gostopolis.Accommodations.Data.Configurations;

public class AccommodationConfiguration : IEntityTypeConfiguration<Accommodation>
{
    public void Configure(EntityTypeBuilder<Accommodation> builder)
    {
        builder
            .HasKey(o => o.Id);

        builder
            .HasOne(a => a.Type)
            .WithMany(t => t.Accommodations)
            .HasForeignKey(a => a.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(a => a.Images)
            .WithOne(i => i.Accommodation)
            .HasForeignKey(i => i.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(a => a.Meals)
            .WithOne(m => m.Accommodation)
            .HasForeignKey(m => m.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(a => a.Rooms)
            .WithOne(r => r.Accommodation)
            .HasForeignKey(r => r.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(a => a.Reservations)
            .WithOne(r => r.Accommodation)
            .HasForeignKey(r => r.AccommodationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}