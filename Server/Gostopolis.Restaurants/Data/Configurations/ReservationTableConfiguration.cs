namespace Gostopolis.Restaurants.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

/// <inheritdoc />
public class ReservationTableConfiguration : IEntityTypeConfiguration<ReservationTable>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ReservationTable> builder)
    {
        builder
            .HasKey(rt => new { rt.ReservationId, rt.TableId });

        builder
            .HasOne(rt => rt.Reservation)
            .WithMany(r => r.ReservationTables)
            .HasForeignKey(rt => rt.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rt => rt.Table)
            .WithMany(t => t.ReservationTables)
            .HasForeignKey(rt => rt.TableId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
