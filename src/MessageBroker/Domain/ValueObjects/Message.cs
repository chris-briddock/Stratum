namespace Domain.ValueObjects;

/// <summary>
/// Represents a message to be published to a topic.
/// </summary>
public sealed class Message
{
    /// <summary>
    /// The title of the message.
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// The content of the message.
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// The timestamp of the message.
    /// </summary>
    public DateTime? TimeStamp { get; set; } = DateTime.UtcNow;
}