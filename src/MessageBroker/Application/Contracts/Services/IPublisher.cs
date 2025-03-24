using System.Threading.Channels;

namespace Application.Contracts;

public interface IPublisher
{
    public Task PublishAsync<TMessage>(string topic,
                                       TMessage message,
                                       Channel<object> channel,
                                       CancellationToken ctx = default) 
                                       where TMessage : class;
}