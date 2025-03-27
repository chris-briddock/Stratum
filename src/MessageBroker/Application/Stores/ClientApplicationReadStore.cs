using Application.Constants;
using Application.Contracts;
using Application.Extensions;
using Application.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides methods for performing read operations on <see cref="ClientApplication"/> entities.
/// </summary>
public sealed class ClientApplicationReadStore : StoreBase, IClientApplicationReadStore
{
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);

    /// <summary>
    /// Initializes a new instance of <see cref="ClientApplicationReadStore"/>
    /// </summary>
    /// <param name="services">An instance of the service provider.</param>
    public ClientApplicationReadStore(IServiceProvider services) : base(services)
    {
    }
    /// <inheritdoc />
 public async Task<List<ClientApplicationDto<string>>> GetClientsAsync(
    int page = 1,
    int pageSize = 10,
    CancellationToken ctx = default)
{
    if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
    if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

    // Create a cache key based on page and pageSize
    string cacheKey = $"clients_{page}_{pageSize}";

     ISpecification<ClientApplication>[] specs = [new ActiveClientSpecification()];

    // Define the compiled query
    var compiledQuery = EF.CompileAsyncQuery(
        (ReadContext context, int skip, int take) =>
            context.Set<ClientApplication>()
            .AsNoTracking()
            .Apply(specs)
            .Select(x => new ClientApplicationDto<string>
            {
                Name = x.Name,
                ApiKey = x.ApiKey,
                Description = x.Description,
                EntityCreationStatus = x.EntityCreationStatus,
                EntityModificationStatus = x.EntityModificationStatus,
                EntityDeletionStatus = x.EntityDeletionStatus
            })
            .Skip(skip)
            .Take(take)
            .ToList()
    );

    // Use FusionCache to retrieve or set the cached data
    var clients = await FusionCache.GetOrSetAsync<List<ClientApplicationDto<string>>>(
        cacheKey,
        async (factory, ctxToken) =>
        {
            factory.Tags = [CacheTagConstants.Clients];
             return await compiledQuery(ReadContext, (page - 1) * pageSize, pageSize);
        },
        token: ctx
    );

    return clients;
}

    /// <inheritdoc />
    public async Task<ClientApplicationDto<string>> GetClientByName(
    string name,
    CancellationToken ctx = default)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "Name must not be null or empty.");

        string cacheKey = "client_by_name_" + name;

        ISpecification<ClientApplication>[] specs = [new ActiveClientSpecification()];

        var compiledQuery = EF.CompileAsyncQuery((ReadContext context) => 
                context.Set<ClientApplication>()
                .AsNoTracking()
                .Apply(specs)
                .Select(x => new ClientApplicationDto<string>()
                {
                    Name = x.Name,
                    ApiKey = x.ApiKey,
                    Description = x.Description,
                    EntityCreationStatus = x.EntityCreationStatus,
                    EntityModificationStatus = x.EntityModificationStatus,
                    EntityDeletionStatus = x.EntityDeletionStatus
                }).Single()); 
        
        var cachedClient = await FusionCache.GetOrSetAsync<ClientApplicationDto<string>>(
            cacheKey,
            async (factory, cancellationToken) =>
            {
                factory.Tags = [CacheTagConstants.ClientByName];
                return await compiledQuery(ReadContext);
            },
            token: ctx
        );

        return cachedClient;
    }
    /// <inheritdoc />
    public async Task<List<ClientApplicationDto<string>>> GetDeletedClients(
        int page = 1,
        int pageSize = 10,
        CancellationToken ctx = default!)
    {
        if (page <= 0)
            throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        string cacheKey = "delete_clients_" + page + "_" + pageSize;
        
        ISpecification<ClientApplication>[] specs = [new InactiveClientSpecification()];

        var compiledQuery = EF.CompileAsyncQuery(
            (ReadContext context, int skip, int take) => 
            context.Set<ClientApplication>()
                   .AsNoTracking()
                   .Apply(specs)
                   .Select(x => new ClientApplicationDto<string>()
                    {
                        Name = x.Name,
                        ApiKey = x.ApiKey,
                        Description = x.Description,
                        EntityCreationStatus = x.EntityCreationStatus,
                        EntityModificationStatus = x.EntityModificationStatus,
                        EntityDeletionStatus = x.EntityDeletionStatus
                    }).ToList());

        var cachedData = await FusionCache.GetOrSetAsync<List<ClientApplicationDto<string>>>(
            cacheKey,
            async (factory, ctxToken) =>
            {
                factory.Tags = [CacheTagConstants.DeletedClients];

                return await compiledQuery(ReadContext, page, pageSize);
            },
            token: ctx
        );

        return cachedData;
    }

}
