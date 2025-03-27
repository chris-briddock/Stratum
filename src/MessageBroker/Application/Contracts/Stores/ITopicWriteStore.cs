using Domain.Entities;

namespace Application.Contracts;

/// <summary>
/// Represents the topic write store contract.
/// </summary>
public interface ITopicWriteStore
{
    /// <summary>
    /// Creates a new topic.
    /// </summary>
    /// <param name="topic">The topic to create.</param>
    /// <param name="ctx">The cancellation token.</param>
    /// <returns>The task representing the operation.</returns>
    Task CreateTopicAsync(Topic topic, CancellationToken ctx);

    /// <summary>
    /// Soft deletes a topic.
    /// </summary>
    /// <param name="topic">The topic to soft delete.</param>
    /// <param name="ctx">The cancellation token.</param>
    /// <returns>The task representing the operation.</returns>
    Task DeleteTopicAsync(Topic topic, CancellationToken ctx);
    /// <summary>
    /// Updates a topic.
    /// </summary>
    /// <param name="topic">The topic to update.</param>
    /// <param name="ctx">The cancellation token.</param>
    /// <returns>The task representing the operation.</returns>
    Task UpdateTopicAsync(Topic topic, CancellationToken ctx);
}