﻿namespace Gostopolis.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .HasKey(m => m.Id);

        builder
            .Property<string>("serializedData")
            .HasField("serializedData");

        builder
            .Property(m => m.Type).IsRequired()
            .HasConversion(
                t => t.AssemblyQualifiedName,
                t => Type.GetType(t));
    }
}
