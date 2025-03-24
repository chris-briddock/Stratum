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
              builder.ToTable("SYSTEM_CLIENT_APPLICATIONS", opt => opt.IsTemporal());

              builder.HasKey(ca => ca.Id);

              builder.Property(ca => ca.Id)
                     .HasMaxLength(36)
                     .HasColumnName("id")
                     .IsRequired();

              builder.Property(ca => ca.SessionId)
                     .HasMaxLength(36)
                     .HasColumnName("session_id")
                     .IsRequired();
                     
              builder.Property(ca => ca.Name)
                     .HasMaxLength(100)
                     .HasColumnName("name")
                     .IsRequired();

              builder.Property(ca => ca.ApiKey)
                     .HasMaxLength(512)
                     .HasColumnName("api_key")
                     .IsRequired();

              builder.HasIndex(ca => ca.ApiKey)
                     .IsUnique();
              
              builder.Property(ca => ca.Description)
                     .HasMaxLength(512)
                     .HasColumnName("description")
                     .IsRequired();

              builder.Property(e => e.ConcurrencyStamp)
                     .HasColumnName("concurrency_stamp")
                     .HasMaxLength(36)
                     .IsConcurrencyToken();

              builder.HasIndex(ca => ca.ConcurrencyStamp)
                     .IsUnique();

              builder.ComplexProperty(u => u.EntityCreationStatus)
                     .Property(x => x.CreatedBy)
                     .HasColumnName("created_by")
                     .HasMaxLength(36);

              builder.ComplexProperty(u => u.EntityCreationStatus)
                     .Property(x => x.CreatedOnUtc)
                     .HasColumnName("created_on_utc")
                     .HasDefaultValueSql("GETUTCDATE()")
                     .ValueGeneratedOnAdd();

              builder.ComplexProperty(u => u.EntityModificationStatus)
                     .Property(x => x.ModifiedBy)
                     .HasColumnName("modified_by")
                     .HasMaxLength(36);

              builder.ComplexProperty(u => u.EntityModificationStatus)
                     .Property(x => x.ModifiedOnUtc)
                     .HasColumnName("modified_on_utc")
                     .HasDefaultValueSql("GETUTCDATE()")
                     .ValueGeneratedOnAddOrUpdate();

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

              // 1:1 client application -> session
              builder.HasOne(ca => ca.Session)
                     .WithOne(s => s.ClientApplication)
                     .HasForeignKey<Session>(s => s.ClientApplicationId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}