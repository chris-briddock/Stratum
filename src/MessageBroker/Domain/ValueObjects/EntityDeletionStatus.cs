namespace Domain.ValueObjects;
/// <summary>
/// Represents soft deletion information for an entity.
/// </summary>
public sealed class EntityDeletionStatus<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Indicates whether the entity is deleted.
    /// </summary>
    public bool IsDeleted { get; private set; } = false;

    /// <summary>
    /// The date and time when the entity was deleted in UTC.
    /// </summary>
    public DateTime? DeletedOnUtc { get; private set; } = null!;

    /// <summary>
    /// The identifier of the user who deleted the entity.
    /// </summary>
    public TKey? DeletedBy { get; private set; } = default!;

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDeletionStatus{TKey}"/>
    /// </summary>
    /// <param name="isDeleted">Indicates whether the entity is deleted.</param>
    /// <param name="deletedOnUtc">The date and time when the entity was deleted in UTC. Null if not deleted.</param>
    /// <param name="deletedBy">The identifier of the user who deleted the entity. Null if not deleted.</param>
    public EntityDeletionStatus(bool isDeleted,
                                DateTime? deletedOnUtc,
                                TKey? deletedBy)
    {
        IsDeleted = isDeleted;
        DeletedOnUtc = deletedOnUtc;
        DeletedBy = deletedBy;
    }
}