
using Application.Contracts;
using Application.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

public sealed class SubscriptionReadStore : StoreBase, ISubscriptionReadStore
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

    /// <inheritdoc />
    public async Task<List<SubscriptionDto>> GetSubscriptionsAsync(int page = 1,
                                                                   int pageSize = 10,
                                                                   CancellationToken cancellationToken = default)
    {
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        return await DbSet
            .AsNoTracking()
            .Select(s => new SubscriptionDto()
            {
                Type = s.Type,
                TopicId = s.TopicId,
                EntityCreationStatus = s.EntityCreationStatus
            })
            .ToListAsync(cancellationToken);
    }
}