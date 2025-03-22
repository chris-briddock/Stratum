using Application.Constants;
using Application.Contracts;
using Application.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides implementation for reading session data from a persistent store.
/// </summary>
public sealed class SessionReadStore : StoreBase, ISessionReadStore
{

    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);
    private DbSet<Session> DbSet => ReadContext.Set<Session>();
    /// <summary>
    /// Initializes a new instance of the <see cref="SessionWriteStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public SessionReadStore(IServiceProvider services) : base(services)
    {
    }
    /// <inheritdoc/>
    public async Task<List<SessionDto>> GetAsync(string userId,
                                                 CancellationToken ctx = default)
    {
        // Use FusionCache to manage caching
        var cacheKey = userId;  // Cache key based on the UserId

        var result = await FusionCache.GetOrSetAsync<List<SessionDto>>(
            cacheKey,
            async (ctx, ct) =>
            {
                ctx.Tags = [CacheTagConstants.Sessions];
                // Query the database if the value is not found in the cache
                return await DbSet
                    .Select(s => new SessionDto
                    {
                        Status = s.Status,
                        ApplicationId = s.ClientApplicationId,
                        UserId = s.UserId,
                        StartDateTime = s.StartDateTime,
                        EndDateTime = s.EndDateTime,
                        UserAgent = s.UserAgent,
                        IpAddress = s.IpAddress
                    })
                    .ToListAsync(ct);
            },
            token: ctx
        );

        return result;
    }
    /// <inheritdoc/>
    public async Task<Session?> GetByIdAsync(string sessionId,
                                             CancellationToken cancellation = default)
    {
        var cacheKey = sessionId;

        var result = await FusionCache.GetOrSetAsync<Session>(
            cacheKey,
            async (ctx, ct) =>
            {
                ctx.Tags = [CacheTagConstants.Sessions];
                var query = await DbSet
                    .Where(x => x.SessionId == sessionId)
                    .FirstOrDefaultAsync(ct);
                // Query the database if the value is not found in the cache
                return query!;
            },
            token: cancellation
        );

        return result;
    }

}