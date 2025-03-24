using Application.Results;
using Domain.Entities;

namespace Application.Contracts;

/// <summary>
/// Defines a contract for a subscription write store.
/// </summary>
public interface ISubscriptionWriteStore
{
    /// <summary>
    /// Creates a new subscription and saves it to the database.
    /// </summary>
    /// <param name="subscription">The subscription to create.</param>
    /// <param name="cancellationToken"> Token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation. </returns>
    Task<SubscriptionResult> CreateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes a subscription from the database.
    /// </summary>
    /// <param name="subscriptionId">The subscription identifier.</param>
    /// <param name="cancellationToken"> Token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation. </returns>
    Task<SubscriptionResult> DeleteSubscriptionAsync(Subscription subscription,
                                                                  CancellationToken cancellationToken = default);
    /// <summary>Updates a subscription in the database.</summary>
    /// <param name="subscription">The subscription to update.</param>
    /// <param name="cancellationToken"> Token for canceling the operation.</param>
    /// <returns>A task representing the asynchronous operation. </returns>
    Task<SubscriptionResult> UpdateSubscriptionAsync(Subscription subscription, CancellationToken cancellationToken = default);
}