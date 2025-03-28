using Application.Constants;
using ChristopherBriddock.AspNetCore.Extensions;
using ChristopherBriddock.AspNetCore.HealthChecks;
using Microsoft.FeatureManagement;


/// <summary>
/// A collection of extension methods for health checks.
/// </summary>
public static partial class HealthCheckExtensions
{
    /// <summary>
    /// Adds a Redis health check to the service collection if the Cache feature flag is enabled.
    /// </summary>
    /// <param name="services">The service collection to which the health check is added.</param>
    /// <param name="configuration">The application's configuration.</param>
    /// <returns>The original service collection, potentially with the Redis health check added.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the required configuration value for Redis connection string is not found.</exception>
    public static IServiceCollection AddRedisHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var featureManager = services.BuildServiceProvider().GetRequiredService<IFeatureManager>();

        if (!featureManager.IsEnabledAsync(FeatureFlagConstants.Cache).Result)
            return services;

        services.AddRedisHealthChecks(configuration.GetRequiredValueOrThrow("ConnectionStrings:Redis"));

        return services;
    }
}