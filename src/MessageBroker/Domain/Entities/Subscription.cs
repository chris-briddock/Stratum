using Domain.Contracts;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents the default implementation of the <see cref="Subscription{TKey}"/> entity.
/// </summary>
public sealed class Subscription : Subscription<string>
{
    /// <summary>
    /// Unique identifier for the subscription entity. 
    /// Defaults to a new GUID represented as a string.
    /// </summary>
    public override string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Identifier for the associated topic of the subscription.
    /// </summary>
    public string TopicId { get; set; } = default!;

    /// <summary>
    /// Associated topic for this subscription.
    /// </summary>
    public Topic Topic { get; set; } = default!;

    /// <summary>
    /// Navigation property for the associated client application.
    /// </summary>
    public ClientApplication ClientApplication { get; set; } = default!;
}

/// <summary>
/// Represents the base class for the <see cref="Subscription"/> entity.
/// </summary>
/// <typeparam name="TKey">The type of the key used to uniquely identify the subscription.</typeparam>
public abstract class Subscription<TKey>
    : IEntityCreationStatus<TKey>,
      IEntityModificationStatus<TKey>,
      IEntityDeletionStatus<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Unique identifier for the subscription entity.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Type of the subscription.
    /// </summary>
    public virtual string Type { get; set; } = default!;

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