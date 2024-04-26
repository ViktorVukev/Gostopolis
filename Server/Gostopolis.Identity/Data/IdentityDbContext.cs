namespace Gostopolis.Identity.Data;

using System.Reflection;
using Gostopolis.Data.Configurations;
using Gostopolis.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

/// <summary>
/// Database context for the identity database. Contains user connected tables.
/// </summary>
/// <param name="options">Context options</param>
public class IdentityDbContext(
    DbContextOptions<IdentityDbContext> options) 
    : IdentityDbContext<User>(options)
{
    public DbSet<Application> Applications { get; set; }

    public DbSet<Message> Messages { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
} 