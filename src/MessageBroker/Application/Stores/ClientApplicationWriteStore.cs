using Application.Constants;
using Application.Contracts;
using Application.Factories;
using Application.Providers;
using Application.Results;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides methods for performing write operations on <see cref="ClientApplication"/> entities.
/// </summary>
public sealed class ClientApplicationWriteStore : StoreBase, IClientApplicationWriteStore
{
    /// <summary>
    /// Gets the <see cref="WriteContext"/> used for database operations.
    /// </summary>
    private WriteContext WriteContext => WriteContextFactory.CreateDbContext(null!);

    /// <summary>
    /// Gets the <see cref="DbSet{TEntity}"/> for <see cref="ClientApplication"/> entities.
    /// </summary>
    private DbSet<ClientApplication> DbSet => WriteContext.Set<ClientApplication>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientApplicationWriteStore"/> class.
    /// </summary>
    /// <param name="services">An instance of the service provider.</param>
    public ClientApplicationWriteStore(IServiceProvider services) : base(services)
    {
        FusionCache.RemoveByTag(CacheTagConstants.ClientByName);
        FusionCache.RemoveByTag(CacheTagConstants.Clients);
        FusionCache.RemoveByTag(CacheTagConstants.DeletedClients);
    }

    /// <inheritdoc />
    public async Task<ClientApplicationResult> CreateAsync(ClientApplication clientApplication,
                                                        CancellationToken ctx = default)
    {
        ArgumentNullException.ThrowIfNull(clientApplication);

        try
        {
            var compiledQuery = EF.CompileAsyncQuery(
                (WriteContext context ) => context.Set<ClientApplication>()
                     .AddAsync(clientApplication, ctx));
            
            await compiledQuery(WriteContext);
            await WriteContext.SaveChangesAsync(ctx);

            return ClientApplicationResult.Success();   
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return ClientApplicationResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);  
        }
        catch (DbUpdateException ex)
        {
            return ClientApplicationResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return ClientApplicationResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
    }

    /// <inheritdoc />
    public async Task<ClientApplicationResult> UpdateAsync(ClientApplication clientApplication,
                                                           CancellationToken ctx = default)
    {
        ArgumentNullException.ThrowIfNull(clientApplication);

        try
        {
            DbSet.Update(clientApplication);
            await WriteContext.SaveChangesAsync(ctx);  
            return ClientApplicationResult.Success();  
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return ClientApplicationResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);  
        }
        catch (DbUpdateException ex)
        {
            return ClientApplicationResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return ClientApplicationResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
        
    }

    /// <inheritdoc />
    public async Task<ClientApplicationResult> DeleteAsync(string clientName, CancellationToken ctx = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(clientName);
        
        try
        {
            var client = await DbSet.SingleOrDefaultAsync(x => x.Name == clientName, ctx);
            if (client is not null)
            {
                client.EntityDeletionStatus = new EntityStatusProvider<string>().Delete("SYSTEM", true, DateTime.UtcNow);
                DbSet.Update(client);
                await WriteContext.SaveChangesAsync(ctx);
            }
            
            return ClientApplicationResult.Success();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return ClientApplicationResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);  
        }
        catch (DbUpdateException ex)
        {
            return ClientApplicationResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return ClientApplicationResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
    }
}