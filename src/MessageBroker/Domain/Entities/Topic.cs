using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents the default implementation of the <see cref="Topic{TKey}"/> entity.
/// </summary>
public sealed class Topic : Topic<string>
{
    /// <summary>
    /// Unique identifier for the topic entity. 
    /// Defaults to a new GUID represented as a string.
    /// </summary>
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Collection of subscriptions associated with the topic.
    /// </summary>
    public ICollection<Subscription> Subscriptions { get; set; } = default!;

    /// <summary>
    /// Collection of events related to the topic.
    /// </summary>
    public ICollection<Event> Events { get; set; } = default!;
}

/// <summary>
/// Represents the base class for the <see cref="Topic"/> entity.
/// </summary>
/// <typeparam name="TKey">The type of the key used for identifying the topic.</typeparam>
public abstract class Topic<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Unique identifier for the topic entity.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Name of the topic.
    /// </summary>
    public virtual string Name { get; set; } = default!;

    /// <summary>
    /// Description of the topic.
    /// </summary>
    public virtual string Description { get; set; } = default!;

    /// <summary>
    /// Status of the topic.
    /// </summary>
    public virtual string Status { get; set; } = default!;

    /// <summary>
    /// Random value that changes whenever the entity is persisted.
    /// </summary>
    public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Creation status of the entity, including information about when and by whom it was created.
    /// </summary>
    public EntityCreationStatus<TKey> EntityCreationStatus { get; set; } = default!;

    /// <summary>
    /// Modification status of the entity, including details of the most recent update and the responsible user.
    /// </summary>
    public EntityModificationStatus<TKey> EntityModificationStatus { get; set; } = default!;

    /// <summary>
    /// Deletion status of the entity, including details of when and by whom it was deleted.
    /// </summary>
    public EntityDeletionStatus<TKey> EntityDeletionStatus { get; set; } = default!;
}