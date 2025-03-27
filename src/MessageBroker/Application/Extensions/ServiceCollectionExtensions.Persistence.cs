using Application.Contracts;
using Application.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Contexts;
using Persistence.Factories;

namespace Application.Extensions;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add persistence layer to the service collection
    /// </summary>
     /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    { 
        services.AddDbContext<BaseContext>(ServiceLifetime.Singleton);
        services.AddDbContext<WriteContext>(ServiceLifetime.Scoped);
        services.AddDbContext<ReadContext>(ServiceLifetime.Scoped);

        services.AddSingleton<IDesignTimeDbContextFactory<ReadContext>, ReadContextFactory>();
        services.AddSingleton<IDesignTimeDbContextFactory<WriteContext>, WriteContextFactory>();

        services.TryAddScoped<ISpecification<ClientApplication>, ActiveClientSpecification>();
        services.TryAddScoped<ISpecification<ClientApplication>, InactiveClientSpecification>();

        return services;
    }
}
