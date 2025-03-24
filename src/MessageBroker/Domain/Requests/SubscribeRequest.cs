namespace Domain.Requests;

/// <summary>
/// Represents a request to subscribe to a specified topic.
/// </summary>
public sealed record SubscribeRequest
{
    /// <summary>
    /// The topic to which the subscriber wants to subscribe.
    /// </summary>
    public string Topic { get; set; }
}