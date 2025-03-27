using Application.Constants;
using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

public sealed class EventReadStore : StoreBase, IEventReadStore
{
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);

    public EventReadStore(IServiceProvider services) : base(services)
    {
    }
    /// <inheritdoc />
    public async Task<List<EventDto<string>>> GetEventsAsync(
        int page = 1,
        int pageSize = 10,
        CancellationToken ctx = default)
    {
        if (page <= 0) throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than zero.");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero.");

        string cacheKey = $"events_{page}_{pageSize}";

        var compiledQuery = EF.CompileAsyncQuery(
            (ReadContext context, int skip, int take) => context.Set<Event>()
                .AsNoTracking()
                .Select(e => new EventDto<string>
                {
                    Type = e.Type,
                    Payload = e.Payload,
                    EntityCreationStatus = e.EntityCreationStatus
                })
                .OrderBy(e => e.EntityCreationStatus.CreatedOnUtc)
                .Skip(skip)
                .Take(take)
                .ToList()
        );

        var events = await FusionCache.GetOrSetAsync<List<EventDto<string>>>(
            cacheKey,
            async (factory, ctxToken) =>
            {
                factory.Tags = [CacheTagConstants.Events];
                return await compiledQuery(ReadContext, (page - 1) * pageSize, pageSize);
            },
            token: ctx
        );

        return events;
    }
    
    /// <inheritdoc />
    public async Task<EventDto<string>> GetEventByType(
        string type,
        CancellationToken ctx = default)
    {
        if (string.IsNullOrEmpty(type))
            throw new ArgumentNullException(nameof(type), "Type must not be null or empty.");

        string cacheKey = $"event_by_type_{type}";

        var compiledQuery = EF.CompileAsyncQuery(
            (ReadContext context) => context.Set<Event>()
                .AsNoTracking()
                .Where(e => e.Type == type)
                .Select(e => new EventDto<string>
                {
                    Type = e.Type,
                    Payload = e.Payload,
                    EntityCreationStatus = e.EntityCreationStatus
                })
                .Single()
        );

        var cachedEvent = await FusionCache.GetOrSetAsync<EventDto<string>>(
            cacheKey,
            async (factory, cancellationToken) =>
            {
                factory.Tags = [CacheTagConstants.Events];
                return await compiledQuery(ReadContext);
            },
            token: ctx
        );

        return cachedEvent;
    }
}