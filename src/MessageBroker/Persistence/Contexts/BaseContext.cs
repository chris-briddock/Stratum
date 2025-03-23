using Domain.Entities;
using MessageBroker;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

/// <summary>
/// Represents the base database context for the application.
/// </summary>
public class BaseContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseContext"/>
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    protected BaseContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseContext"/>
    /// </summary>
    public BaseContext() : base()
    {
    }

    /// <summary>
    /// Configures the model by applying configurations from the assembly 
    /// containing the <see cref="Program"/> class.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// The collection of client applications within the context.
    /// </summary>
    public DbSet<ClientApplication> Applications => Set<ClientApplication>();

    /// <summary>
    /// The collection of events within the context.
    /// </summary>
    public DbSet<Event> Events => Set<Event>();

    /// <summary>
    /// The collection of sessions within the context.
    /// </summary>
    public DbSet<Session> Sessions => Set<Session>();

    /// <summary>
    /// The collection of subscriptions within the context.
    /// </summary>
    public DbSet<Subscription> Subscriptions => Set<Subscription>();

    /// <summary>
    /// The collection of topics within the context.
    /// </summary>
    public DbSet<Topic> Topics => Set<Topic>();
}