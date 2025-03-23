using Domain.Contracts;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represent a default implementation for an <see cref="Event{Tkey}"/> 
/// </summary>
public sealed class Event : Event<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string TopicId { get; set; } = default!; 
    public Topic Topic { get; set; } = default!;
}
/// <summary>
/// Represents a base class for an <see cref="Event"/> entity.
/// </summary>
/// <typeparam name="TKey">The type of the key used for identifying the session and client application.</typeparam>
public abstract class Event<TKey> : IEntityCreationStatus<TKey> 
where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the event entity.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;
    /// <summary>
    /// Gets or sets the event type.
    /// </summary>
    public virtual string Type { get; set; } = default!;
    /// <summary>
    /// Gets or sets the event payload.
    /// </summary>
    public virtual string Payload { get; set; } = default!;
    /// <summary>
    /// A random value that should change whenever the entity is persisted.
    /// </summary>
    public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    /// <summary>
    /// Gets or sets the creation status.
    /// </summary>
    public EntityCreationStatus<TKey> EntityCreationStatus { get; set; } = default!;
}