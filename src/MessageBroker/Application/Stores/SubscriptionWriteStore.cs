
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

public sealed class SubscriptionWriteStore : StoreBase
{
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);

    private DbSet<Subscription> DbSet => ReadContext.Set<Subscription>();

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionWriteStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used for dependency injection.</param>
    public SubscriptionWriteStore(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// Creates a new subscription and saves it to the database.
    /// </summary>
    /// <param name="subscription">The subscription to create.</param>
    /// <param name="cancellationToken">Token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the subscription is null.</exception>
    public async Task CreateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);

        await DbSet.AddAsync(subscription, cancellationToken);
        await ReadContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing subscription in the database.
    /// </summary>
    /// <param name="subscription">The subscription to update.</param>
    /// <param name="cancellationToken">Token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the subscription is null.</exception>
    public async Task UpdateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);

        DbSet.Update(subscription);
        await ReadContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a subscription from the database by its ID.
    /// </summary>
    /// <param name="subscriptionId">The ID of the subscription to delete.</param>
    /// <param name="cancellationToken">Token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the subscription ID is null or empty.</exception>
    public async Task DeleteSubscriptionAsync(string subscriptionId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(subscriptionId);

        var subscription = await DbSet.FirstOrDefaultAsync(s => s.Id == subscriptionId, cancellationToken);
        if (subscription != null)
        {
            DbSet.Remove(subscription);
            await ReadContext.SaveChangesAsync(cancellationToken);
        }
    }
}