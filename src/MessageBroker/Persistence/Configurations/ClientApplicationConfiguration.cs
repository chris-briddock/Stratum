using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

/// <summary>
/// Configures the database schema for the <see cref="ClientApplication"/> entity.
/// Implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </summary>
public sealed class ClientApplicationConfiguration : IEntityTypeConfiguration<ClientApplication>
{
    /// <summary>
    /// Configures the entity of type <see cref="ClientApplication"/>.
    /// </summary>
    /// <param name="builder">The builder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<ClientApplication> builder)
    {
        // Configure the table name and temporal settings.
        builder.ToTable("SYSTEM_CLIENT_APPLICATIONS", opt => opt.IsTemporal());

        // Configure the primary key.
        builder.HasKey(ca => ca.Id);

        // Configure the Id property.
        builder.Property(ca => ca.Id)
               .HasMaxLength(36)
               .HasColumnName("id")
               .IsRequired();

        // Configure the Name property.
        builder.Property(ca => ca.Name)
               .HasMaxLength(100)
               .HasColumnName("name")
               .IsRequired();

        // Configure the ApiKey property.
        builder.Property(ca => ca.ApiKey)
               .HasMaxLength(512)
               .HasColumnName("api_key")
               .IsRequired();

        // Create a unique index on the ApiKey property.
        builder.HasIndex(ca => ca.ApiKey)
               .IsUnique();

        // Configure the ConcurrencyStamp property.
        builder.Property(e => e.ConcurrencyStamp)
               .HasColumnName("concurrency_stamp")
               .HasMaxLength(36)
               .IsConcurrencyToken();

        // Create a unique index on the ConcurrencyStamp property.
        builder.HasIndex(ca => ca.ConcurrencyStamp)
               .IsUnique();

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

        // Configure properties related to entity modification status.
        builder.ComplexProperty(u => u.EntityModificationStatus)
               .Property(x => x.ModifiedBy)
               .HasColumnName("modified_by")
               .HasMaxLength(36);

        builder.ComplexProperty(u => u.EntityModificationStatus)
               .Property(x => x.ModifiedOnUtc)
               .HasDefaultValueSql("GETUTCDATE()")
               .ValueGeneratedOnAddOrUpdate();

        // Configure properties related to entity deletion status.
        builder.ComplexProperty(u => u.EntityDeletionStatus)
               .Property(x => x.DeletedBy)
               .HasColumnName("deleted_by")
               .HasMaxLength(36);

        builder.ComplexProperty(u => u.EntityDeletionStatus)
               .Property(x => x.DeletedOnUtc)
               .HasColumnName("deleted_on_utc")
               .HasMaxLength(36);

        builder.ComplexProperty(u => u.EntityDeletionStatus)
               .Property(x => x.IsDeleted)
               .HasColumnName("is_deleted")
               .IsRequired();

        // Configure the one-to-one relationship with the Session entity.
        builder.HasOne(ca => ca.Session)
            .WithOne(s => s.ClientApplication)
            .HasForeignKey<Session>(s => s.ClientApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure the one-to-many relationship with the Subscription entity.
        builder.HasMany(ca => ca.Subscriptions)
            .WithOne(s => s.ClientApplication)
            .OnDelete(DeleteBehavior.Cascade);
    }
}