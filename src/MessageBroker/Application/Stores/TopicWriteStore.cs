using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Application.Stores;

/// <summary>
/// Represents the write store for the <see cref="Topic"/> entity.
/// </summary>
public sealed class TopicWriteStore : StoreBase, ITopicWriteStore
{
    /// <summary>
    /// The write context.
    /// </summary>
    private WriteContext WriteContext { get; }
    /// <summary>
    /// Initializes a new instance of <see cref="TopicWriteStore"/>
    /// </summary>
    /// <param name="services">The service provider.</param>
    /// <param name="writeContext">The write context.</param>
    public TopicWriteStore(IServiceProvider services,
                           WriteContext writeContext) : base(services)
    {
        WriteContext = writeContext;
    }
    private static readonly Func<WriteContext, string, Task<Topic?>> GetTopicByNameCompiledQuery =
        EF.CompileAsyncQuery((WriteContext context, string name) =>
            context.Topics.FirstOrDefault(t => t.Name == name));

    /// <inheritdoc/>
    public async Task CreateTopicAsync(Topic topic,
                                    CancellationToken ctx)
    {
        await WriteContext.Topics.AddAsync(topic, ctx);
        await WriteContext.SaveChangesAsync(ctx);
    }
    /// <inheritdoc/>
    public async Task UpdateTopicAsync(Topic topic,
                                       CancellationToken ctx)
    {
        WriteContext.Topics.Update(topic);
        await WriteContext.SaveChangesAsync(ctx);
    }
    /// <inheritdoc/>
    public async Task DeleteTopicAsync(Topic topic,
                                       CancellationToken ctx)
    {
        WriteContext.Topics.Remove(topic);
        await WriteContext.SaveChangesAsync(ctx);
    }
}