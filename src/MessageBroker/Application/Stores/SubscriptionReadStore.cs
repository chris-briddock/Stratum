
using Application.Dtos;
using Application.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

public sealed class SubscriptionReadStore : StoreBase
{
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);

    private DbSet<Subscription> DbSet => ReadContext.Set<Subscription>();
    /// <summary>
    /// Initializes a new instance of the <see cref="SubscriptionReadStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used for dependency injection.</param>
    public SubscriptionReadStore(IServiceProvider services) : base(services)
    {
    }

    /// </summary>
    /// <param name="page">The page number (1-based).</param>
    /// <param name="pageSize">The number of subscriptions per page.</param>
    /// <param name="cancellationToken">Token for canceling the operation.</param>
    /// <returns>A paginated list of subscriptions.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the page or pageSize is less than or equal to zero.</exception>
    public async Task<PaginatedList<SubscriptionDto>> GetSubscriptionsAsync(int page = 1,
                                                                            int pageSize = 10,
                                                                            CancellationToken cancellationToken = default)
    {
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        return await DbSet
            .Select(s => new SubscriptionDto
            {
                Type = s.Type,
                TopicId = s.TopicId,
                EntityCreationStatus = s.EntityCreationStatus
            })
            .ToPaginatedListAsync(page, pageSize, cancellationToken);
    }
}