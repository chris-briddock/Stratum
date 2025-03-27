namespace Domain.Events;

/// <summary>
/// Event to subscribe to a topic
/// </summary>
/// <param name="Topic">The topic to subscribe to.</param>
public sealed record SubscribeEvent(string Topic);