using System.Collections.Concurrent;
using System.Threading.Channels;
using Application.Contracts;
using ChristopherBriddock.AspNetCore.Extensions;

namespace MessageBroker.Application.Services;

public sealed class ChannelManager : IChannelManager
{
    private ConcurrentDictionary<string, object> Channels { get; }

    public IConfiguration Configuration { get; }

    public ChannelManager(IConfiguration configuration)
    {
        Channels = new();
        Configuration = configuration;
    }

    public async IAsyncEnumerable<string> GetActiveTopics()
    {
        foreach (var topic in Channels.Keys)
        {
            yield return topic;
            await Task.Yield();
        }
    }
    /// <inheritdoc/>
    public int GetMessageCount(string topic)
    {
        if (Channels.TryGetValue(topic, out var channelObj) && channelObj is Channel<object> channel)
        {
            return channel.Reader.Count;
        }

        throw new KeyNotFoundException($"The topic '{topic}' does not exist.");
    }
    /// <inheritdoc/>
    public Channel<T> GetOrCreateTopicChannel<T>(string name)
    {
        return (Channel<T>)Channels.GetOrAdd(name, _ =>
            Channel.CreateBounded<T>(new BoundedChannelOptions(Convert.ToInt16(Configuration.GetRequiredValueOrThrow("Channels:Capacity")))
            {
                SingleReader = false,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.DropOldest
            }));
    }
    /// <inheritdoc/>
    public Channel<Queue<T>> GetOrCreateQueueChannel<T>(string name)
    {
        return (Channel<Queue<T>>)Channels.GetOrAdd(name, _ =>
            Channel.CreateBounded<Queue<T>>(new BoundedChannelOptions(Convert.ToInt16(Configuration.GetRequiredValueOrThrow("Channels:Capacity")))
            {
                SingleReader = true,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.Wait
            }));
    }
    /// <inheritdoc/>
    public bool RemoveChannel(string name)
    {
        if (Channels.TryRemove(name, out var channelObj) && channelObj is Channel<object> channel)
        {
            channel.Writer.TryComplete();
            return true;
        }
        return false;
    }
}