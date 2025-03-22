using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("SYSTEM_EVENTS", opt => opt.IsTemporal());

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
               .HasMaxLength(36)
               .HasColumnName("id")
               .IsRequired();

        builder.Property(e => e.Type)
               .HasMaxLength(120)
               .HasColumnName("type")
               .IsRequired();
        
        builder.Property(e => e.Payload)
               .HasColumnName("payload")
               .IsRequired();
        
        builder.Property(e => e.ConcurrencyStamp)
               .HasColumnName("concurrency_stamp")
               .HasMaxLength(36)
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

        builder.HasIndex(ca => ca.ConcurrencyStamp)
               .IsUnique();

        builder.HasOne(e => e.Topic)
               .WithMany(t => t.Events)
               .HasForeignKey(e => e.TopicId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
