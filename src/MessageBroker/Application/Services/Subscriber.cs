using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Application.Contracts;

namespace Application.Services;

public sealed class Subscriber : ISubscriber
{
    public async IAsyncEnumerable<T> SubscribeAsync<T>(Channel<object> channel, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (await channel.Reader.WaitToReadAsync(cancellationToken))
        {
            while (channel.Reader.TryRead(out var message))
            {
                if (message is T typedMessage)
                {
                    yield return typedMessage;
                }
            }
        }
    }
}