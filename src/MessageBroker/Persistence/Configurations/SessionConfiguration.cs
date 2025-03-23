using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

/// <summary>
/// Configures the properties and relationships of the <see cref="Session"/> entity.
/// </summary>
public sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    /// <summary>
    /// Configures the entity of type <see cref="Session"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("SYSTEM_SESSIONS", opt => opt.IsTemporal());

        builder.Property(e => e.Id)
               .HasColumnName("id")
               .HasMaxLength(36);

        builder.Property(e => e.SessionId)
               .HasColumnName("session_id")
               .HasMaxLength(36);

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .HasMaxLength(36);

        builder.Property(e => e.StartDateTime)
               .HasColumnName("start_date_time")
               .IsRequired();

        builder.Property(e => e.EndDateTime)
               .HasColumnName("end_date_time");

        builder.Property(e => e.IpAddress)
               .HasColumnName("ip_address")
               .HasMaxLength(45);

        builder.Property(e => e.UserAgent)
               .HasColumnName("user_agent")
               .HasMaxLength(256);

        builder.Property(e => e.Status)
               .HasColumnName("status")
               .HasMaxLength(100);

        builder.ComplexProperty(u => u.EntityCreationStatus)
               .Property(x => x.CreatedBy)
               .HasColumnName("created_by")
               .HasMaxLength(36);

        builder.ComplexProperty(u => u.EntityCreationStatus)
               .Property(x => x.CreatedOnUtc)
               .HasColumnName("created_on_utc")
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
       
       builder.Property(e => e.ConcurrencyStamp)
              .HasColumnName("concurrency_stamp")
              .HasMaxLength(36)
              .IsConcurrencyToken(); 
       
       // 1:1 Session -> ClientApplication (Restrict to avoid multiple cascading paths)
       builder.HasOne(s => s.ClientApplication)
              .WithOne(ca => ca.Session)
              .HasForeignKey<ClientApplication>(ca => ca.SessionId)
              .OnDelete(DeleteBehavior.Restrict);
       
    }
}