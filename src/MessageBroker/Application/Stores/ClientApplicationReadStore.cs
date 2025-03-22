
using Application.Contracts;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Stores;

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
    public async Task<PaginatedList<ClientApplicationDto<string>>> GetClientsAsync(int page = 1,
                                                                                   int pageSize = 10,
                                                                                   CancellationToken ctx = default)
    {
        return await DbSet.Select(x => new ClientApplicationDto<string>()
        {
            Name = x.Name,
            ApiKey = x.ApiKey,
            Description = x.Description,
            EntityCreationStatus = x.EntityCreationStatus,
            EntityModificationStatus = x.EntityModificationStatus,
            EntityDeletionStatus = x.EntityDeletionStatus
        }).ToPaginatedListAsync(page, pageSize, ctx);
    }
    /// <inheritdoc />
    public async Task<ClientApplicationDto<string>> GetClientByName(string name, CancellationToken ctx = default)
    {
        return await DbSet.Where(x => x.Name == name)
                          .Select(x => new ClientApplicationDto<string>()
                          {
                              Name = x.Name,
                              ApiKey = x.ApiKey,
                              Description = x.Description,
                              EntityCreationStatus = x.EntityCreationStatus,
                              EntityModificationStatus = x.EntityModificationStatus,
                              EntityDeletionStatus = x.EntityDeletionStatus
                          }).SingleAsync(ctx);
    }
    /// <inheritdoc />
    public async Task<PaginatedList<ClientApplicationDto<string>>> GetDeletedClients(int page = 1,
                                                                                     int pageSize = 10,
                                                                                     CancellationToken ctx = default!)
    {
        return await DbSet.Where(x => x.EntityDeletionStatus.IsDeleted == true)
                         .Select(x => new ClientApplicationDto<string>()
                         {
                             Name = x.Name,
                             ApiKey = x.ApiKey,
                             Description = x.Description,
                             EntityCreationStatus = x.EntityCreationStatus,
                             EntityModificationStatus = x.EntityModificationStatus,
                             EntityDeletionStatus = x.EntityDeletionStatus
                         }).ToPaginatedListAsync(page, pageSize, ctx);
    }

}
