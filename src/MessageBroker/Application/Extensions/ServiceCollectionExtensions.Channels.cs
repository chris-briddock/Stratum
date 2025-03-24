using Application.Contracts;
using MessageBroker.Application.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Application.Extensions;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageBrokerChannels(this IServiceCollection services)
    {
        services.TryAddSingleton<IChannelManager, ChannelManager>();
        return services;
    }
}