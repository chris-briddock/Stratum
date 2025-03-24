using Application.Dtos;
using Application.Extensions;

namespace Application.Contracts;

/// <summary>
/// Represents the subscription read store contract. 
/// </summary>
public interface ISubscriptionReadStore
{
    /// <summary>
    /// Retrieves a paginated list of subscriptions.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A paginated list of subscriptions.</returns>
    Task<PaginatedList<SubscriptionDto>> GetSubscriptionsAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
}