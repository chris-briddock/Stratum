using Domain.Entities;
using MessageBroker;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public abstract class BaseContext : DbContext
{
    public IConfiguration Configuration { get; }
    protected BaseContext(DbContextOptions options,
                          IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    public BaseContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"), opt => opt.EnableRetryOnFailure());
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<ClientApplication> Applications => Set<ClientApplication>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Topic> Topics => Set<Topic>();

}