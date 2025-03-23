using Application.Constants;
using Application.DTOs;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides methods for performing read operations on <see cref="Event"/> entities.
/// </summary>
public sealed class EventReadStore : StoreBase
{
    /// <summary>
    /// Gets the <see cref="ReadContext"/> used for database operations.
    /// </summary>
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);

    /// <summary>
    /// Represents the collection of events in the database.
    /// </summary>
    private DbSet<Event> DbSet => ReadContext.Set<Event>();

    /// <summary>
    /// Initializes a new instance of <see cref="EventReadStore"/>
    /// </summary>
    /// <param name="services">An instance of the service provider.</param>
    public EventReadStore(IServiceProvider services) : base(services)
    {
    }

    /// <summary>
    /// Retrieves events asynchronously with pagination, utilizing FusionCache for caching.
    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="pageSize">The number of events per page.</param>
    /// <param name="ctx">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation, containing a paginated list of events.</returns>
    public async Task<PaginatedList<EventDto<string>>> GetEventsAsync(int page = 1, 
                                                                      int pageSize = 10, 
                                                                      CancellationToken ctx = default)
    {
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        string cacheKey = "events-page-" + page + "-size-" + pageSize;

        return await FusionCache.GetOrSetAsync<PaginatedList<EventDto<string>>>(
            cacheKey,
            async (ctx, cancellationToken) =>
            {
                // Setting tags for cache invalidation if necessary
                ctx.Tags = [CacheTagConstants.Events ];

                // Fetching events from the database and projecting to EventDto
                var events = await DbSet
                    .Select(x => new EventDto<string>()
                    {
                        Type = x.Type,
                        Payload = x.Payload,
                        EntityCreationStatus = x.EntityCreationStatus
                    })
                    .ToPaginatedListAsync(page, pageSize, cancellationToken);

                return events;
            },
            token: ctx
        );
    }
}