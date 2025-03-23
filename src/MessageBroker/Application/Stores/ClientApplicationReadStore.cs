
using Application.Constants;
using Application.Contracts;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Stores;

/// <summary>
/// Provides methods for performing read operations on <see cref="ClientApplication"/> entities.
/// </summary>
public sealed class ClientApplicationReadStore : StoreBase, IClientApplicationReadStore
{
    private DbSet<ClientApplication> DbSet => ReadContextFactory.CreateDbContext(null!).Set<ClientApplication>();
    /// <summary>
    /// Initializes a new instance of <see cref="ClientApplicationReadStore"/>
    /// </summary>
    /// <param name="services">An instance of the service provider.</param>
    public ClientApplicationReadStore(IServiceProvider services) : base(services)
    {
    }
    /// <inheritdoc />
    public async Task<PaginatedList<ClientApplicationDto<string>>> GetClientsAsync(
    int page = 1,
    int pageSize = 10,
    CancellationToken ctx = default)
    {
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        // Create a cache key based on page and pageSize
        string cacheKey = $"clients_{page}_{pageSize}";

        var clients = await FusionCache.GetOrSetAsync<PaginatedList<ClientApplicationDto<string>>>(
            cacheKey,
            async (cacheCtx, cacheToken) =>
            {
                cacheCtx.Tags = [CacheTagConstants.Clients];

                // Fetch data from DbSet and project to ClientApplicationDto
                var clientList = await DbSet.Select(x => new ClientApplicationDto<string>()
                {
                    Name = x.Name,
                    ApiKey = x.ApiKey,
                    Description = x.Description,
                    EntityCreationStatus = x.EntityCreationStatus,
                    EntityModificationStatus = x.EntityModificationStatus,
                    EntityDeletionStatus = x.EntityDeletionStatus
                }).ToPaginatedListAsync(page, pageSize, cacheToken);

                return clientList;
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

        var cachedClient = await FusionCache.GetOrSetAsync<ClientApplicationDto<string>>(
            cacheKey,
            async (cacheCtx, cacheToken) =>
            {
                cacheCtx.Tags = [CacheTagConstants.ClientByName];

                // Fetch data from DbSet and project to ClientApplicationDto
                return await DbSet.Where(x => x.Name == name)
                                  .Select(x => new ClientApplicationDto<string>()
                                  {
                                      Name = x.Name,
                                      ApiKey = x.ApiKey,
                                      Description = x.Description,
                                      EntityCreationStatus = x.EntityCreationStatus,
                                      EntityModificationStatus = x.EntityModificationStatus,
                                      EntityDeletionStatus = x.EntityDeletionStatus
                                  })
                                  .SingleAsync(cacheToken); // Fetch single record
            },
            token: ctx
        );

        return cachedClient;
    }
    /// <inheritdoc />
    public async Task<PaginatedList<ClientApplicationDto<string>>> GetDeletedClients(
        int page = 1,
        int pageSize = 10,
        CancellationToken ctx = default!)
    {
        if (page <= 0)
            throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        string cacheKey = "delete_clients_" + page + "_" + pageSize;

        var cachedData = await FusionCache.GetOrSetAsync<PaginatedList<ClientApplicationDto<string>>>(
            cacheKey,
            async (cacheCtx, cacheToken) =>
            {
                cacheCtx.Tags = [CacheTagConstants.DeletedClients];

                // Fetch data from DbSet and project to ClientApplicationDto
                return await DbSet.Where(x => x.EntityDeletionStatus.IsDeleted == true)
                                  .Select(x => new ClientApplicationDto<string>()
                                  {
                                      Name = x.Name,
                                      ApiKey = x.ApiKey,
                                      Description = x.Description,
                                      EntityCreationStatus = x.EntityCreationStatus,
                                      EntityModificationStatus = x.EntityModificationStatus,
                                      EntityDeletionStatus = x.EntityDeletionStatus
                                  })
                                  .ToPaginatedListAsync(page, pageSize, cacheToken);
            },
            token: ctx
        );

        return cachedData;
    }

}
