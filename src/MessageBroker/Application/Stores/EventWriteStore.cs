
using Application.Constants;
using Application.Contracts;
using Application.Factories;
using Application.Results;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides data access methods for storing and updating events.
/// </summary>
public sealed class EventWriteStore : StoreBase, IEventWriteStore
{
    /// <summary>
    /// The database context used for performing write operations.
    /// </summary>
    private WriteContext WriteContext => WriteContextFactory.CreateDbContext(null!);

    /// <summary>
    /// Represents the collection of events in the database.
    /// </summary>
    private DbSet<Event> DbSet => WriteContext.Set<Event>();

    /// <summary>
    /// Creates an instance of the <see cref="EventWriteStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used for dependency injection.</param>
    public EventWriteStore(IServiceProvider services) : base(services)
    {
    }

    /// <inheritdoc/>
    public async Task<EventResult> AddEventAsync(Event eventEntity, CancellationToken cancellationToken = default)
    {
        if (eventEntity is null)
        {
            throw new ArgumentNullException(nameof(eventEntity), "Event cannot be null.");
        }

        try
        {
            await DbSet.AddAsync(eventEntity, cancellationToken);
            await WriteContext.SaveChangesAsync(cancellationToken);
            return EventResult.Success();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return EventResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);
        }
        catch (DbUpdateException ex)
        {
            return EventResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return EventResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
    }
    /// <inheritdoc/>
    public async Task<EventResult> UpdateEventAsync(Event eventEntity,
                                                    CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(eventEntity);

        try
        {
            DbSet.Update(eventEntity);
            await WriteContext.SaveChangesAsync(cancellationToken);
            return EventResult.Success();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return EventResult.Failed([ErrorFactory.DbUpdateConcurrencyException(ex.Message)]);
        }
        catch (DbUpdateException ex)
        {
            return EventResult.Failed([ErrorFactory.DbUpdateException(ex.Message)]);
        }
        catch (OperationCanceledException ex)
        {
            return EventResult.Failed(ErrorFactory.OperationCancelled(ex.Message));
        }
    }
}
