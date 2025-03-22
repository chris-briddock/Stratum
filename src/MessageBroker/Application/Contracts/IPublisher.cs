using System.Threading.Channels;

namespace Application.Contracts;

public interface IPublisher
{
    public Task PublishAsync<T>(string topic,
                                T message,
                                Channel<object> channel);
}