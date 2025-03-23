namespace Domain.Events;

/// <summary>
/// Represents an event that is published to a specific topic.
/// </summary>
/// <param name="Topic">The topic to which the event is published.</param>
public sealed record PublishEvent(string Topic);