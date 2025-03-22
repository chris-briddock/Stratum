using System.Threading.Channels;
using Application.Contracts;

namespace Application.Services;

public sealed class Publisher : IPublisher
{
    public async Task PublishAsync<T>(string topic,
                                      T message,
                                      Channel<object> channel)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(topic, nameof(topic));
        ArgumentNullException.ThrowIfNull(message, nameof(message));
        
        if (!await channel.Writer.WaitToWriteAsync())
            throw new InvalidOperationException($"Unable to write to topic '{topic}'");
        
        await channel.Writer.WriteAsync(message);
    }
}
