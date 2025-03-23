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
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);
    private DbSet<Event> DbSet => ReadContext.Set<Event>();

    /// <summary>
    /// Initializes a new instance of <see cref="EventReadStore"/>
    /// </summary>
    /// <param name="services">An instance of the service provider.</param>
    public EventReadStore(IServiceProvider services) : base(services)
    {
    }

    /// <inheritdoc />
public async Task<PaginatedList<EventDto<string>>> GetEventsAsync(int page = 1,
                                                                  int pageSize = 10,
                                                                  CancellationToken ctx = default)
{
    // Validate parameters
    if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
    if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

    return await DbSet.Select(x => new EventDto<string>()
        {
            Type = x.Type,
            Payload = x.Payload,
            EntityCreationStatus = x.EntityCreationStatus,
            
        }).ToPaginatedListAsync(page, pageSize, ctx);
}
}