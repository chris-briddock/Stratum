using System.Threading.Channels;

namespace Application.Contracts;

/// <summary>
/// Represents a contract for publishing messages to a channel.
/// </summary>
public interface IPublisher
{
    /// <summary>
    /// Publishes a message to a channel.
    /// </summary>
    /// <typeparam name="TMessage">The type of the message to publish.</typeparam>
    /// <param name="topic">The topic to which the message is published. </param>
    /// <param name="message">The message to publish.</param>
    /// <param name="channel">The channel to which the message is published.</param>
    /// <param name="ctx">The cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task PublishAsync<TMessage>(string topic,
                                       TMessage message,
                                       Channel<object> channel,
                                       CancellationToken ctx = default) 
                                       where TMessage : class;
}