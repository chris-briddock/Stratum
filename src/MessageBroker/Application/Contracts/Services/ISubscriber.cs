using System.Threading.Channels;

namespace Application.Contracts;

/// <summary>
/// Represents a subscriber that can subscribe to a channel and receive messages.
/// </summary>
/// <typeparam name="T">The type of the message.</typeparam>
public interface ISubscriber<T> where T : class
{
    /// <summary>
    /// Subscribes to a channel and receives messages.
    /// </summary>
    /// <typeparam name="TMessage">
    /// The type of the message to receive. 
    /// </typeparam>
    /// <param name="channel">
    /// The channel to subscribe to.</param>
    /// <param name="cancellationToken">
    /// The cancellation token to cancel the operation. 
    /// </param>
    /// <returns>
    /// An asynchronous enumerable that yields messages of the specified type. 
    /// </returns>
    IAsyncEnumerable<T> SubscribeAsync<TMessage>(Channel<T> channel,
                                          CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Unsubscribes from a channel.
    /// </summary>
    /// <param name="channel">
    /// The channel to unsubscribe from. 
    // </param>
    /// <param name="cancellationToken">
    /// The cancellation token to cancel the operation. 
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// </returns>
    Task UnsubscribeAsync(Channel<T> channel,
                         CancellationToken cancellationToken = default);
}