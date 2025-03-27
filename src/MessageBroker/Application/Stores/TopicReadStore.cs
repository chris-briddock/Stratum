using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

public sealed class TopicReadStore : StoreBase, ITopicReadStore
{
    private ReadContext ReadContext { get; }
    public TopicReadStore(IServiceProvider services,
                          ReadContext readContext) : base(services)
    {
        ReadContext = readContext;
    }
    /// <inheritdoc/>
    public async Task<List<Topic>> GetTopicsAsync(int page = 1,
                                                  int pageSize = 10,
                                                  CancellationToken ctx = default)
    {
        return await ReadContext.Topics
                                .AsNoTracking()
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync(ctx);
    }
    /// <inheritdoc/>
    public async Task<Topic?> GetTopicByNameAsync(string name,
                                                 CancellationToken ctx)
    {
        return await ReadContext.Topics
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Name == name, ctx);
    }


}