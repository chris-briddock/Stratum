using Application.Constants;
using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Provides implementation for writing session data to a persistent store.
/// </summary>

public sealed class SessionWriteStore : StoreBase, ISessionWriteStore
{
    private WriteContext WriteContext => WriteContextFactory.CreateDbContext(null!);
    private ReadContext ReadContext => ReadContextFactory.CreateDbContext(null!);
    private DbSet<Session> WriteDbSet => WriteContext.Set<Session>();
    private DbSet<Session> ReadDbSet => ReadContext.Set<Session>();
    /// <summary>
    /// Initializes a new instance of the <see cref="SessionWriteStore"/> class.
    /// </summary>
    /// <param name="services">The service provider used to resolve dependencies.</param>
    public SessionWriteStore(IServiceProvider services) : base(services)
    {
        FusionCache.RemoveByTag(CacheTagConstants.Sessions);
    }

    /// <inheritdoc />
    public async Task<Session> CreateAsync(Session session)
    {
        var strategy = WriteContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            try
            {
                await WriteDbSet.AddAsync(session);
                await WriteContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                var existingSession = await ReadDbSet.AsNoTracking()
                    .FirstOrDefaultAsync(s => s.SessionId == session.SessionId);

                if (existingSession is not null)
                    return;

                await WriteContext.Sessions.AddAsync(session);
                await WriteContext.SaveChangesAsync();
            }
        });

        return session;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Session session)
    {
        try
        {

            await WriteDbSet.Where(x => x.SessionId == session.SessionId)
                .ExecuteUpdateAsync(x => x
                .SetProperty(s => s.EntityDeletionStatus.IsDeleted, true)
                .SetProperty(s => s.EndDateTime, DateTime.UtcNow)
                .SetProperty(s => s.EntityDeletionStatus.DeletedBy, session.UserId)
                .SetProperty(s => s.EntityDeletionStatus.DeletedOnUtc, DateTime.UtcNow)
                .SetProperty(s => s.Status, SessionStatus.Terminated));
        }
        catch (Exception)
        {
            throw;
        }

    }
}