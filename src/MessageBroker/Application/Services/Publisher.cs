using System.Threading.Channels;
using Application.Contracts;

namespace Application.Services;

/// <summary>
/// Represents a service for publishing messages to a channel.
/// </summary>
public sealed class Publisher : IPublisher
{
    /// <inheritdoc/>
    public async Task PublishAsync<TMessage>(string topic,
                                             TMessage message,
                                             Channel<object> channel,
                                             CancellationToken ctx) 
                                             where TMessage : class
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(topic, nameof(topic));
        ArgumentNullException.ThrowIfNull(message, nameof(message));
        
        if (!await channel.Writer.WaitToWriteAsync(ctx))
            throw new InvalidOperationException($"Unable to write to topic '{topic}'");
        
        await channel.Writer.WriteAsync(message, ctx);
    }
}
