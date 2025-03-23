using Application.Results;
using Domain.Entities;

namespace Application.Contracts;

/// <summary>
/// Defines a contract for writing event data to a persistent store, 
/// including operations for adding and updating event entities asynchronously.
/// </summary>
public interface IEventWriteStore
{
    /// <summary>
    /// Adds a new event to the database asynchronously.
    /// </summary>
    /// <param name="eventEntity">The event entity to be added.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>A task that completes when the event has been added.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the event entity is null.</exception>
    Task<EventResult> AddEventAsync(Event eventEntity,
                                    CancellationToken cancellationToken = default);
    /// <summary>
    /// Updates an existing event in the database asynchronously.
    /// </summary>
    /// <param name="eventEntity">The event entity to be updated.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>A task that completes when the event has been updated.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the event entity is null.</exception>
    Task<EventResult> UpdateEventAsync(Event eventEntity,
                                       CancellationToken cancellationToken = default);
}
