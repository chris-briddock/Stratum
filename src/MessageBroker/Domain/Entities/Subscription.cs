using Domain.Contracts;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a default implementation of the <see cref="Subscription{TKey}"/> entity.
/// </summary>
public sealed class Subscription : Subscription<string>
{
    /// <summary>
    /// Gets or sets the unique identifier for the subscription.
    /// Defaults to a new GUID represented as a string.
    /// </summary>
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the topic identifier.
    /// </summary>
    public string TopicId { get; set; } = default!;
    /// <summary>
    /// Gets or sets the associated topic for this subscription.
    /// </summary>
    public Topic Topic { get; set; } = default!;
    /// <summary>
    /// Gets or sets the client application navigation property.
    /// </summary>
    public ClientApplication ClientApplication { get; set; } = default!;
}

/// <summary>
/// Represents a base class for the <see cref="Subscription"/> entity.
/// </summary>
/// <typeparam name="TKey">The type of the key used for identifying the subscription.</typeparam>
public abstract class Subscription<TKey> 
    : IEntityCreationStatus<TKey>,
      IEntityModificationStatus<TKey>,
      IEntityDeletionStatus<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the subscription.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the type of the subscription.
    /// </summary>
    public virtual string Type { get; set; } = default!;

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