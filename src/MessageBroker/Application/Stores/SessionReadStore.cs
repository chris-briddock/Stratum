using Application.Constants;
using Application.Contracts;
using Application.Dtos;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides implementation for reading session data from a persistent store.
/// </summary>
public sealed class SessionReadStore : StoreBase, ISessionReadStore
{
    /// <summary>
    /// The database context used for performing read operations.
    /// </summary>
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="SessionReadStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public SessionReadStore(IServiceProvider services) : base(services)
    {
    }

    /// <inheritdoc/>
    public async Task<List<SessionDto>> GetAsync(string applicationId, CancellationToken ctx = default)
    {
        ArgumentNullException.ThrowIfNull(applicationId);

        string cacheKey = $"sessions_{applicationId}";

        var compiledQuery = EF.CompileAsyncQuery(
            (ReadContext context) => context.Set<Session>()
                .AsNoTracking()
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
                .ToList()
        );

        var result = await FusionCache.GetOrSetAsync<List<SessionDto>>(
            cacheKey,
            async (factory, token) =>
            {
                factory.Tags = [CacheTagConstants.Sessions];
                return await compiledQuery(ReadContext);
            },
            token: ctx
        );

        return result;
    }

    /// <inheritdoc/>
    public async Task<Session?> GetByIdAsync(string sessionId, CancellationToken cancellation = default)
    {
        if (string.IsNullOrEmpty(sessionId))
            throw new ArgumentNullException(nameof(sessionId), "Session ID must not be null or empty.");
        
        string cacheKey = $"session_{sessionId}";

        var compiledQuery = EF.CompileAsyncQuery(
            (ReadContext context, string id) => context.Set<Session>()
                .AsNoTracking()
                .Where(x => x.SessionId == id)
                .FirstOrDefault()
        );

        var result = await FusionCache.GetOrSetAsync<Session?>(
            cacheKey,
            async (factory, token) =>
            {
                factory.Tags = [CacheTagConstants.Sessions];
                return await compiledQuery(ReadContext, sessionId);
            },
            token: cancellation
        );

        return result;
    }
}