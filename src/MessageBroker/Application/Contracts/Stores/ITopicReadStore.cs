using Domain.Entities;

namespace Application.Stores;

/// <summary>
/// Represents a contract for a read store of topics
/// </summary>
public interface ITopicReadStore
{
    /// <summary>
    /// Get a topic by name
    /// </summary>
    /// <param name="name">The name of the topic</param>
    /// <param name="ctx">The cancellation token </param>
    /// <returns>The topic if found, otherwise null</returns>
    Task<Topic?> GetTopicByNameAsync(string name, CancellationToken ctx);
    /// <summary>
    /// Get a paginated list of topics
    /// </summary>
    /// <param name="page">The page number to retrieve </param>
    /// <param name="pageSize">The number of items to retrieve per page </param>
    /// <param name="ctx">The cancellation token</param>
    /// <returns>A paginated list of topics</returns>
    Task<List<Topic>> GetTopicsAsync(int page = 0, int pageSize = 10, CancellationToken ctx = default);
}