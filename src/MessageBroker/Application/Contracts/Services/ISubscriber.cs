using System.Threading.Channels;

namespace Application.Contracts;

public interface ISubscriber<T> where T : class
{
    IAsyncEnumerable<T> SubscribeAsync<TMessage>(Channel<T> channel,
                                          CancellationToken cancellationToken = default);
    Task UnsubscribeAsync(Channel<T> channel,
                         CancellationToken cancellationToken = default);
}