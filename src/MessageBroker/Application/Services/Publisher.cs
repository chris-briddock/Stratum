using System.Threading.Channels;
using Application.Contracts;

namespace Application.Services;

public sealed class Publisher : IPublisher
{
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
