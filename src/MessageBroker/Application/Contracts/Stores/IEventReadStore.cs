using Application.DTOs;
using Application.Extensions;

namespace Application.Contracts;

/// <summary>
/// Provides methods for performing read operations on <see cref="Event"/> entities.
/// </summary>
public interface IEventReadStore
{
    /// <summary> Retrieves events asynchronously with pagination.</summary>
    /// <param name="page"> The page number (1-based). </param>
    /// <param name="pageSize">The number of events per page.</param>
    /// <param name="ctx">The cancellation token.</param>
    /// <returns> 
    // A task that represents the asynchronous operation, 
    // containing a paginated list of events.
    /// </returns>
    Task<PaginatedList<EventDto<string>>> GetEventsAsync(int page = 1, int pageSize = 10, CancellationToken ctx = default);
}