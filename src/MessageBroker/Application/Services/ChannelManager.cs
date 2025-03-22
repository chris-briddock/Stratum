using System.Collections.Concurrent;
using System.Threading.Channels;
using Application.Contracts;

namespace MessageBroker.Application.Services;

public sealed class ChannelManager : IChannelManager, IDisposable
{
    private ConcurrentDictionary<string, object> Channels { get; }

    public ChannelManager()
    {
        Channels = new();
    }

    public IEnumerable<string> GetActiveTopics()
    {
        return Channels.Keys;
    }

    public int GetMessageCount(string topic)
    {
        if (Channels.TryGetValue(topic, out var channelObj) && channelObj is Channel<object> channel)
        {
            return channel.Reader.Count;
        }

        throw new KeyNotFoundException($"The topic '{topic}' does not exist.");
    }

    public Channel<T> GetOrCreateTopicChannel<T>(string name, int capacity)
    {
        return (Channel<T>)Channels.GetOrAdd(name, _ =>
            Channel.CreateBounded<T>(new BoundedChannelOptions(capacity)
            {
                SingleReader = false,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.DropOldest
            }));
    }

    public Channel<Queue<T>> GetOrCreateQueueChannel<T>(string name, int capacity)
    {
        return (Channel<Queue<T>>)Channels.GetOrAdd(name, _ =>
            Channel.CreateBounded<Queue<T>>(new BoundedChannelOptions(capacity)
            {
                SingleReader = true,
                SingleWriter = false,
                FullMode = BoundedChannelFullMode.Wait
            }));
    }

    public bool RemoveChannel(string name)
    {
        if (Channels.TryRemove(name, out var channelObj) && channelObj is Channel<object> channel)
        {
            channel.Writer.TryComplete();
            return true;
        }
        return false;
    }

    public void Dispose()
    {
        foreach (var channelObj in Channels.Values)
        {
            if (channelObj is Channel<object> channel)
            {
                channel.Writer.TryComplete();
            }
        }
        Channels.Clear();
    }
}