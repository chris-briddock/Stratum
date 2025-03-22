using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("SYSTEM_SUBSCRIPTIONS", opt => opt.IsTemporal());

        builder.Property(e => e.Id)
               .HasColumnName("id")
               .HasMaxLength(36);

        builder.Property(e => e.Type)
               .HasMaxLength(100)
               .HasColumnName("type")
               .IsRequired();

        builder.Property(e => e.ConcurrencyStamp)
               .HasMaxLength(36)
               .HasColumnName("concurrency_stamp")
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
               .HasDefaultValueSql("GETUTCDATE()")
               .ValueGeneratedOnAdd();

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

         builder.HasIndex(ca => ca.ConcurrencyStamp)
                .IsUnique();

        builder.HasOne(e => e.Topic)
               .WithMany()
               .HasForeignKey("topic_id")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.ClientApplication)
               .WithMany()
               .OnDelete(DeleteBehavior.Cascade);

    }
}
