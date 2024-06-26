﻿namespace Gostopolis.Data;

using System.Reflection;
using Configurations;
using Microsoft.EntityFrameworkCore;
using Models;

public abstract class MessageDbContext : DbContext
{
    protected MessageDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; }

    protected abstract Assembly ConfigurationsAssembly { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(this.ConfigurationsAssembly);

        base.OnModelCreating(modelBuilder);
    }
}
