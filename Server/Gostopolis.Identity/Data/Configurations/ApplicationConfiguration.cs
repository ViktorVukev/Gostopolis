namespace Gostopolis.Identity.Data.Configurations;

using Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Data model configuration of Application class
/// </summary>
public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
{
    /// <summary>
    /// Configure the Application data model.
    /// </summary>
    /// <param name="builder">Database builder</param>
    public void Configure(EntityTypeBuilder<Application> builder)
    {
        builder
            .HasKey(r => r.Id);

        builder
            .HasOne(a => a.User)
            .WithMany(u => u.Applications)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
