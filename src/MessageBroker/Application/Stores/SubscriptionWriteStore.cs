
using Application.Contracts;
using Application.Factories;
using Application.Results;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

public sealed class SubscriptionWriteStore : StoreBase, ISubscriptionWriteStore
{
    private WriteContext WriteContext => WriteContextFactory.CreateDbContext(null!);

    private DbSet<Subscription> DbSet => WriteContext.Set<Subscription>();

    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionWriteStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used for dependency injection.</param>
    public SubscriptionWriteStore(IServiceProvider services) : base(services)
    {
    }

    /// <inheritdoc/>
    public async Task<SubscriptionResult> CreateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);

        try
        {
            await DbSet.AddAsync(subscription, cancellationToken);
            await WriteContext.SaveChangesAsync(cancellationToken);

            return SubscriptionResult.Success();

        }
        catch (DbUpdateConcurrencyException ex)
        {
            return SubscriptionResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);
        }
        catch (DbUpdateException ex)
        {
            return SubscriptionResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return SubscriptionResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }

    }

    /// <inheritdoc/>
    public async Task<SubscriptionResult> UpdateSubscriptionAsync(Subscription subscription,
                                                                  CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);
        try
        {
            DbSet.Update(subscription);
            await WriteContext.SaveChangesAsync(cancellationToken);

            return SubscriptionResult.Success();

        }
        catch (DbUpdateConcurrencyException ex)
        {
            return SubscriptionResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);
        }
        catch (DbUpdateException ex)
        {
            return SubscriptionResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return SubscriptionResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
    }

    /// <inheritdoc/>
    public async Task<SubscriptionResult> DeleteSubscriptionAsync(Subscription subscription,
                                                                  CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);

        try
        {
            var entity = await DbSet.FirstOrDefaultAsync(s => s.Id == subscription.Id, cancellationToken);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await WriteContext.SaveChangesAsync(cancellationToken);
            }
            return SubscriptionResult.Success();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return SubscriptionResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);
        }
        catch (DbUpdateException ex)
        {
            return SubscriptionResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return SubscriptionResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
    }
}