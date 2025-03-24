using System.Threading.Channels;

namespace Application.Contracts;

public interface ISubscriber
{
    IAsyncEnumerable<T> SubscribeAsync<T>(Channel<object> channel, CancellationToken cancellationToken = default);
}