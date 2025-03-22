using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a default implementation for the <see cref="Topic{Key}"/> entity.
/// </summary>
public sealed class Topic : Topic<string>
{
    /// <summary>
    /// Gets or sets the unique identifier for the topic. 
    /// Defaults to a new GUID represented as a string.
    /// </summary>
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the collection of subscriptions associated with the topic.
    /// </summary>
    public ICollection<Subscription> Subscriptions { get; set; } = default!;
    
    public ICollection<Event> Events { get; set; } = default!;
}
/// <summary>
/// Represents a base class for the <see cref="Topic"/> entity.
/// </summary>
/// <typeparam name="TKey">The type of the key used for identifying the session and client application.</typeparam>
public abstract class Topic<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the topic.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the topic.
    /// </summary>
    public virtual string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the description of the topic.
    /// </summary>
    public virtual string Description { get; set; } = default!;

    /// <summary>
    /// Gets or sets the status of the topic.
    /// </summary>
    public virtual string Status { get; set; } = default!;

    /// <summary>
    /// A random value that should change whenever the entity is persisted.
    /// </summary>
    public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the creation status of the entity, including information about 
    /// when the entity was created and by whom.
    /// </summary>
    public EntityCreationStatus<TKey> EntityCreationStatus { get; set; } = default!;

    /// <summary>
    /// Gets or sets the modification status of the entity, including information about 
    /// the most recent update and the user responsible.
    /// </summary>
    public EntityModificationStatus<TKey> EntityModificationStatus { get; set; } = default!;

    /// <summary>
    /// Gets or sets the deletion status of the entity, including information about 
    /// when the entity was deleted and by whom.
    /// </summary>
    public EntityDeletionStatus<TKey> EntityDeletionStatus { get; set; } = default!;
}