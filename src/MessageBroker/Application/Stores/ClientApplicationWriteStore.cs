using Application.Contracts;
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
    }

    /// <summary>
    /// Adds a new <see cref="ClientApplication"/> entity to the database.
    /// </summary>
    /// <param name="clientApplication">The <see cref="ClientApplication"/> entity to add.</param>
    /// <param name="ctx">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddAsync(ClientApplication clientApplication, CancellationToken ctx = default)
    {
        
        await DbSet.AddAsync(clientApplication, ctx);
        await WriteContext.SaveChangesAsync(ctx);
    }

    /// <summary>
    /// Updates an existing <see cref="ClientApplication"/> entity in the database.
    /// </summary>
    /// <param name="clientApplication">The <see cref="ClientApplication"/> entity to update.</param>
    /// <param name="ctx">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task UpdateAsync(ClientApplication clientApplication, CancellationToken ctx = default)
    {
        DbSet.Update(clientApplication);
        await WriteContext.SaveChangesAsync(ctx);
    }

    /// <summary>
    /// Deletes a <see cref="ClientApplication"/> entity from the database by its name.
    /// </summary>
    /// <param name="clientName">The name of the <see cref="ClientApplication"/> to delete.</param>
    /// <param name="ctx">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DeleteAsync(string clientName, CancellationToken ctx = default)
    {
        var client = await DbSet.SingleOrDefaultAsync(x => x.Name == clientName, ctx);
        if (client != null)
        {
            DbSet.Remove(client);
            await WriteContext.SaveChangesAsync(ctx);
        }
    }
}