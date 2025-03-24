using System.Threading.Channels;

namespace Application.Contracts;

/// <summary>
/// Provides methods for managing channels associated with topics and queues.
/// </summary>
public interface IChannelManager
{
    /// <summary>
    /// Retrieves or creates a channel for a queue associated with a specific topic.
    /// </summary>
    /// <typeparam name="T">The type of messages in the queue.</typeparam>
    /// <param name="topic">The topic associated with the queue channel.</param>
    /// <returns>A channel for the queue.</returns>
    Channel<Queue<T>> GetOrCreateQueueChannel<T>(string topic);

    /// <summary>
    /// Retrieves or creates a channel for a specific topic.
    /// </summary>
    /// <typeparam name="T">The type of messages in the topic channel.</typeparam>
    /// <param name="topic">The topic for which the channel is created or retrieved.</param>
    /// <returns>A channel for the topic.</returns>
    Channel<T> GetOrCreateTopicChannel<T>(string topic);

    /// <summary>
    /// Removes the channel associated with the given topic.
    /// </summary>
    /// <param name="topic">The topic whose channel is to be removed.</param>
    /// <returns>A boolean indicating whether the removal was successful.</returns>
    bool RemoveChannel(string topic);

    /// <summary>
    /// Retrieves the list of active topics currently being managed.
    /// </summary>
    /// <returns>An enumerable collection of active topics.</returns>
    IAsyncEnumerable<string> GetActiveTopics();

    /// <summary>
    /// Retrieves the current message count for a specific topic.
    /// </summary>
    /// <param name="topic">The topic whose message count is to be retrieved.</param>
    /// <returns>The number of messages in the specified topic.</returns>
    int GetMessageCount(string topic);
}

