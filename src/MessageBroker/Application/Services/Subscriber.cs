using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Application.Contracts;

namespace Application.Services;

public sealed class Subscriber : ISubscriber<object>
{


    public async IAsyncEnumerable<object> SubscribeAsync<TMessage>(Channel<object> channel,
                                                                   [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(channel);

        while (await channel.Reader.WaitToReadAsync(cancellationToken))
        {
            while (channel.Reader.TryRead(out var message))
            {
                if (message is TMessage typedMessage)
                {
                    yield return typedMessage;
                }
            }
        }
    }

    public async Task UnsubscribeAsync(Channel<object> channel,
                                       CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(channel);

        channel.Writer.Complete();
            await Task.CompletedTask;
    }
}