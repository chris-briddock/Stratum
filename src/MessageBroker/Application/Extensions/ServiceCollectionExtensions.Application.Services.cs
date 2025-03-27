using Application.Contracts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.Extensions;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the DI container
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.TryAddScoped<IPublisher, Publisher>();
        services.TryAddScoped<ISubscriber<object>, Subscriber>();
        services.TryAddSingleton<IChannelManager, ChannelManager>();

        return services; 
    }
}