using Application.Contracts;
using Application.Stores;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Contexts;
using Persistence.Factories;

namespace Application.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    { 
        services.AddDbContext<BaseContext>(ServiceLifetime.Singleton);
        services.AddDbContext<WriteContext>(ServiceLifetime.Scoped);
        services.AddDbContext<ReadContext>(ServiceLifetime.Scoped);

        services.AddSingleton<IDesignTimeDbContextFactory<ReadContext>, ReadContextFactory>();
        services.AddSingleton<IDesignTimeDbContextFactory<WriteContext>, WriteContextFactory>();

        services.TryAddScoped<ISessionReadStore, SessionReadStore>();
        services.TryAddScoped<ISessionWriteStore, SessionWriteStore>();
        services.TryAddScoped<IClientApplicationReadStore, ClientApplicationReadStore>();
        services.TryAddScoped<IClientApplicationWriteStore, ClientApplicationWriteStore>();
        services.TryAddScoped<IEventReadStore, EventReadStore>();
        services.TryAddScoped<IEventWriteStore, EventWriteStore>();   
        services.TryAddScoped<ISubscriptionReadStore, SubscriptionReadStore>();
        services.TryAddScoped<ISubscriptionWriteStore, SubscriptionWriteStore>();     

        return services;
    }
}
