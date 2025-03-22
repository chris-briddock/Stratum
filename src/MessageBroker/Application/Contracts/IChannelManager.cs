using System.Threading.Channels;

namespace Application.Contracts;

  public interface IChannelManager
    {
        Channel<Queue<T>> GetOrCreateQueueChannel<T>(string topic, int capacity);
        Channel<T> GetOrCreateTopicChannel<T>(string topic, int capacity);
        bool RemoveChannel(string topic);
        IEnumerable<string> GetActiveTopics();
        int GetMessageCount(string topic);
    }
