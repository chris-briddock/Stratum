using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Configures the database schema for the <see cref="Topic"/> entity.
/// Implements the <see cref="IEntityTypeConfiguration{TEntity}"/> interface.
/// </summary>
public sealed class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
       /// <summary>
       /// Configures the entity of type <see cref="Topic"/>.
       /// </summary>
       /// <param name="builder">The builder used to configure the entity.</param>
       public void Configure(EntityTypeBuilder<Topic> builder)
       {
              builder.ToTable("SYSTEM_TOPICS", opt => opt.IsTemporal());

              builder.HasKey(t => t.Id);

              builder.Property(t => t.Id)
                     .HasColumnName("id")
                     .HasMaxLength(36)
                     .IsRequired();

              builder.Property(t => t.Name)
                     .HasColumnName("name")
                     .HasMaxLength(255)
                     .IsRequired();

              builder.Property(t => t.Description)
                     .HasColumnName("description")
                     .HasMaxLength(1000)
                     .IsRequired();

              builder.Property(t => t.Status)
                     .HasColumnName("status")
                     .HasMaxLength(100)
                     .IsRequired();

              builder.Property(t => t.ConcurrencyStamp)
                     .HasColumnName("concurrency_stamp")
                     .IsRequired()
                     .IsConcurrencyToken();

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

              // Configure the relationship: 1:N Topic -> Events (Cascade delete)
              builder.HasMany(t => t.Events)
                     .WithOne(e => e.Topic)
                     .HasForeignKey(e => e.TopicId)
                     .OnDelete(DeleteBehavior.Cascade); // Ensure cascading delete from Topic -> Events

              // Configure the relationship: 1:N Topic -> Subscriptions (Cascade delete)
              builder.HasMany(t => t.Subscriptions)
                     .WithOne(sub => sub.Topic)
                     .HasForeignKey(sub => sub.TopicId)
                     .OnDelete(DeleteBehavior.Cascade);
       }
}