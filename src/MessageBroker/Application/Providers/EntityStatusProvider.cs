using Application.Contracts;
using Domain.ValueObjects;

namespace Application.Providers;

/// <summary>
/// Provides factory methods to create and manage entity status objects.
/// </summary>
/// <typeparam name="TKey">The type of the identifier for the entity.</typeparam>
public sealed class EntityStatusProvider<TKey> : 
    IEntityStatusProvider<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Creates a new <see cref="EntityCreationStatus{TKey}"/> with the current UTC timestamp.
    /// </summary>
    /// <param name="createdBy">The identifier of the user or process that created the entity.</param>
    /// <returns>A new instance of <see cref="EntityCreationStatus{TKey}"/>.</returns>
    public EntityCreationStatus<TKey> Create(TKey createdBy)
    {
        return new EntityCreationStatus<TKey>(DateTime.UtcNow, createdBy);
    }

    /// <summary>
    /// Creates a new <see cref="EntityModificationStatus{TKey}"/> with the current UTC timestamp.
    /// </summary>
    /// <param name="modifiedBy">The identifier of the user or process that modified the entity.</param>
    /// <returns>A new instance of <see cref="EntityModificationStatus{TKey}"/>.</returns>
    public EntityModificationStatus<TKey> Update(TKey modifiedBy)
    {
        return new EntityModificationStatus<TKey>(DateTime.UtcNow, modifiedBy);
    }

    /// <summary>
    /// Creates a new <see cref="EntityDeletionStatus{TKey}"/> indicating the entity has been deleted, with the current UTC timestamp.
    /// </summary>
    /// <param name="deletedBy">The identifier of the user or process that deleted the entity.</param>
    /// <returns>A new instance of <see cref="EntityDeletionStatus{TKey}"/> marked as deleted.</returns>
    public EntityDeletionStatus<TKey> Delete(TKey deletedBy)
    {
        return new EntityDeletionStatus<TKey>(true, DateTime.UtcNow, deletedBy);
    }

    /// <summary>
    /// Creates a new <see cref="EntityDeletionStatus{TKey}"/> indicating the entity has been restored, with the current UTC timestamp.
    /// </summary>
    /// <param name="restoredBy">The identifier of the user or process that restored the entity.</param>
    /// <returns>A new instance of <see cref="EntityDeletionStatus{TKey}"/> marked as not deleted.</returns>
    public EntityDeletionStatus<TKey> Restore(TKey restoredBy)
    {
        return new EntityDeletionStatus<TKey>(false, DateTime.UtcNow, restoredBy);
    }
}