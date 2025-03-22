using Application.Contracts;
using Application.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence.Contexts;

namespace Application.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    { 
        services.AddDbContext<BaseContext>();
        services.AddDbContextFactory<WriteContext>();
        services.AddDbContextFactory<ReadContext>(opt => opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.TryAddScoped<ISessionReadStore, SessionReadStore>();
        services.TryAddScoped<ISessionWriteStore, SessionWriteStore>();
        services.TryAddScoped<IClientApplicationReadStore, ClientApplicationReadStore>();
        

        return services;
    }
}
