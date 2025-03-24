namespace Domain.Requests;

/// <summary>
/// Represents the request to unsubscribe from a topic.
/// </summary>
public sealed record UnsubscribeRequest
{
    public string TopicName { get; init; }
    /// <summary>
    /// The name of the subscription to remove.
    /// </summary>
    public string SubscriptionName { get; init; }
}