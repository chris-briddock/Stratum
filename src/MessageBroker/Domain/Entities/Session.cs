using Domain.ValueObjects;

namespace Domain.Entities;

/// <summary>
/// Represents the default implementation for the <see cref="Session{TKey}"/> entity.
/// </summary>
public sealed class Session : Session<string>
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string ClientApplicationId { get; set; } = default!;

    /// <summary>
    /// Navigation property for the associated client application.
    /// </summary>
    public ClientApplication ClientApplication { get; set; } = default!;
}

/// <summary>
/// Represents a user session within the system.
/// </summary>
/// <typeparam name="TKey">The type of the key used to uniquely identify the session.</typeparam>
public abstract class Session<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Unique identifier for the session entity.
    /// </summary>
    public virtual TKey Id { get; set; } = default!;

    /// <summary>
    /// Identifier for the session.
    /// </summary>
    public virtual string SessionId { get; set; } = default!;

    /// <summary>
    /// Identifier for the user associated with the session.
    /// </summary>
    public virtual string UserId { get; set; } = default!;

    /// <summary>
    /// Date and time when the session started.
    /// </summary>
    public DateTime StartDateTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when the session ended, or null if it is still active.
    /// </summary>
    public DateTime? EndDateTime { get; set; } = default!;

    /// <summary>
    /// IP address from which the session originated.
    /// </summary>
    public virtual string? IpAddress { get; set; } = default!;

    /// <summary>
    /// User agent string from the client that initiated the session.
    /// </summary>
    public virtual string UserAgent { get; set; } = default!;

    /// <summary>
    /// Status of the session.
    /// </summary>
    public virtual string Status { get; set; } = default!;

    /// <summary>
    /// Random value that changes whenever the entity is persisted.
    /// </summary>
    public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Deletion status of the entity.
    /// </summary>
    public EntityDeletionStatus<string> EntityDeletionStatus { get; set; } = default!;

    /// <summary>
    /// Creation status of the entity.
    /// </summary>
    public EntityCreationStatus<string> EntityCreationStatus { get; set; } = default!;
}