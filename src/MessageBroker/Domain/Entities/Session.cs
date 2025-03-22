using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents a default implementation for the <see cref="Session{TKey}"/> entity.
/// </summary>
public sealed class Session : Session<string>
{
    public string ClientApplicationId { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets client application navigation property.
    /// </summary>
    public ClientApplication ClientApplication { get; set; } = default!;
}

/// <summary>
/// Represents a user session within the system.
/// </summary>
public abstract class Session<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the session entity.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    public virtual string SessionId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user identifier associated with the session
    /// </summary>
    public virtual string UserId { get; set; } = default!;

    /// <summary>
    /// Gets or sets the start time of the session.
    /// </summary>
    public DateTime StartDateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the end time of the session.
    /// </summary>
    public DateTime? EndDateTime { get; set; } = default!;

    /// <summary>
    /// Gets or sets the IP address from which the originated.
    /// </summary>
    public virtual string? IpAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets the user agent string of the client that initiated the session.
    /// </summary>
    public virtual string UserAgent { get; set; } = default!;

    /// <summary>
    /// Gets or sets the current status of the session.
    /// </summary>
    public virtual string Status { get; set; } = default!;

    /// <summary>
    /// A random value that should change whenever the entity is persisted.
    /// </summary>
    public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the deletion status of the entity.
    /// </summary>
    public EntityDeletionStatus<string> EntityDeletionStatus { get; set; } = default!;

    /// <summary>
    /// Gets or sets the creation status of the entity.
    /// </summary>
    public EntityCreationStatus<string> EntityCreationStatus { get; set; } = default!;
}