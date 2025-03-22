using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;
using Persistence.Factories;
using ZiggyCreatures.Caching.Fusion;

namespace Application.Stores;

/// <summary>
/// Provides a base class for data stores with common service dependencies.
/// </summary>
public abstract class StoreBase
{
    /// <summary>
    /// Gets the service provider used to resolve dependencies.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    /// Gets the <see cref="ReadContext"/> instance from the service provider.
    /// This context is used to interact with the application's database when reading.
    /// </summary>
    public IDesignTimeDbContextFactory<ReadContext> ReadContextFactory => Services.GetRequiredService<IDesignTimeDbContextFactory<ReadContext>>();
    /// <summary>
    /// Gets the <see cref="WriteContext"/> instance from the service provider.
    /// This context is used to interact with the application's database when writing.
    /// </summary>
    public IDesignTimeDbContextFactory<WriteContext> WriteContextFactory => Services.GetRequiredService<IDesignTimeDbContextFactory<WriteContext>>();

    
    public IFusionCache FusionCache => Services.GetRequiredService<IFusionCache>();

    /// <summary>
    /// Initializes a new instance of the <see cref="StoreBase"/> class.
    /// </summary>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    protected StoreBase(IServiceProvider services)
    {
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

}