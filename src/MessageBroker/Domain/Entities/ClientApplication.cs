using Domain.Contracts;
using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a default implementation of the 
/// <see cref="ClientApplication{Tkey}"/> entity.
/// </summary>
public sealed class ClientApplication : ClientApplication<string>
{
    /// <summary>
    /// Gets or sets the unique identifier for the client application.
    /// Defaults to a new GUID represented as a string.
    /// </summary>
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
     public string SessionId { get; set; } = default!;
    /// <summary>
    /// Gets or sets the session associated with this client application.
    /// </summary>
    public Session Session { get; set;} = default!;
    /// <summary>
    /// Gets or sets the collection of subscriptions that is used as a nagivation property.
    /// </summary>
    public ICollection<Subscription> Subscriptions { get; set; } = [];
}
public abstract class ClientApplication<TKey> : IEntityCreationStatus<TKey>,
                                                IEntityModificationStatus<TKey>,
                                                IEntityDeletionStatus<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;
    /// <summary>
    /// Gets or sets the name for the application.
    /// </summary>
    public virtual string Name { get; set; } = default!;
    /// <summary>
    /// Gets or sets the api key for the application.
    /// </summary>
    public virtual string ApiKey { get; set; } = default!;
    /// <summary>
    /// Gets or sets the description of the application.
    /// </summary>
    public virtual string Description { get; set; } = default!;
     /// <summary>
    /// A random value that should change whenever the entity is persisted.
    /// </summary>
    public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    /// <summary>
    /// Gets or sets the creation status of the application
    /// </summary>
    public EntityCreationStatus<TKey> EntityCreationStatus { get; set; } = default!;
    /// <summary>
    /// Gets or sets the modification status of the application
    /// </summary>
    public EntityModificationStatus<TKey> EntityModificationStatus { get; set; } = default!;
    /// <summary>
    /// Gets or sets the deletion status of the application
    /// </summary>
    public EntityDeletionStatus<TKey> EntityDeletionStatus { get; set; } = default!;
}