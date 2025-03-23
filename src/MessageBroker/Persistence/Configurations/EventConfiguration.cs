using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="Event"/> entity.
/// Implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </summary>
public sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    /// <summary>
    /// Configures the entity of type <see cref="Event"/>.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        // Configure the table name and temporal settings.
        builder.ToTable("SYSTEM_EVENTS", opt => opt.IsTemporal());

        // Configure the primary key.
        builder.HasKey(e => e.Id);

        // Configure the Id property.
        builder.Property(e => e.Id)
               .HasMaxLength(36)
               .HasColumnName("id")
               .IsRequired();

        // Configure the Type property.
        builder.Property(e => e.Type)
               .HasMaxLength(120)
               .HasColumnName("type")
               .IsRequired();

        // Configure the Payload property.
        builder.Property(e => e.Payload)
               .HasColumnName("payload")
               .IsRequired();

        // Configure the ConcurrencyStamp property.
        builder.Property(e => e.ConcurrencyStamp)
               .HasColumnName("concurrency_stamp")
               .HasMaxLength(36)
               .IsConcurrencyToken();

        // Configure properties related to entity creation status.
        builder.ComplexProperty(u => u.EntityCreationStatus)
               .Property(x => x.CreatedBy)
               .HasColumnName("created_by")
               .HasMaxLength(36);

        builder.ComplexProperty(u => u.EntityCreationStatus)
               .Property(x => x.CreatedOnUtc)
               .HasColumnName("created_on_utc")
               .HasDefaultValueSql("GETUTCDATE()")
               .ValueGeneratedOnAdd();

        // Create a unique index on the ConcurrencyStamp property.
        builder.HasIndex(ca => ca.ConcurrencyStamp)
               .IsUnique();

       builder.Property(x => x.TopicId)
              .HasMaxLength(36)
              .HasColumnName("topic_id")
              .IsRequired();

        builder.HasOne(e => e.Topic)
               .WithMany(t => t.Events)
               .HasForeignKey(e => e.TopicId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

