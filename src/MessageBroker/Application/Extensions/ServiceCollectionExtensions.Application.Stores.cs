using Application.Contracts;
using Application.Stores;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.Extensions;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add stores to the service collection
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.TryAddScoped<ISessionReadStore, SessionReadStore>();
        services.TryAddScoped<ISessionWriteStore, SessionWriteStore>();
        services.TryAddScoped<IClientApplicationReadStore, ClientApplicationReadStore>();
        services.TryAddScoped<IClientApplicationWriteStore, ClientApplicationWriteStore>();
        services.TryAddScoped<IEventReadStore, EventReadStore>();
        services.TryAddScoped<IEventWriteStore, EventWriteStore>();   
        services.TryAddScoped<ISubscriptionReadStore, SubscriptionReadStore>();
        services.TryAddScoped<ISubscriptionWriteStore, SubscriptionWriteStore>(); 
        services.TryAddScoped<ITopicReadStore, TopicReadStore>();
        services.TryAddScoped<ITopicWriteStore, TopicWriteStore>();

        return services; 
    }
}