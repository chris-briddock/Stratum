using Domain.ValueObjects;

namespace Domain.Requests;

/// <summary>
/// Represents a request to publish a message to a specified topic.
/// </summary>
public sealed record PublishRequest
{
    /// <summary>
    /// The topic to which the message will be published.
    /// </summary>
    public string Topic { get; init; } = default!;

    /// <summary>
    /// The type of the event being published.
    /// </summary>
    public string EventType { get; set; } = default!;

    /// <summary>
    /// The message being published.
    /// </summary>
    public Message Message { get; init; } = default!;
}