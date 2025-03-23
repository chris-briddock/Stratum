using Domain.ValueObjects;

namespace Application.Dtos;

/// <summary>
/// Represents a data transfer object for a subscription.
/// </summary>
public class SubscriptionDto
{
    /// <summary>
    /// The type of the subscription.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// The unique identifier of the associated topic.
    /// </summary>
    public string TopicId { get; set; } = default!;

    /// <summary>
    /// Contains the creation status information of the entity.
    /// </summary>
    public EntityCreationStatus<string> EntityCreationStatus { get; set; } = default!;
}
