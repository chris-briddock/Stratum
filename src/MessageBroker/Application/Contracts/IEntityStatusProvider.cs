using Domain.ValueObjects;

namespace Application.Contracts;

/// <summary>
/// Defines a contract for managing the status of an entity, including creation, modification, deletion, and restoration.
/// </summary>
/// <typeparam name="TKey">
/// The type of the key that uniquely identifies the entity. Must implement <see cref="IEquatable{T}"/>.
/// </typeparam>
public interface IEntityStatusProvider<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Restores a previously deleted entity.
    /// </summary>
    /// <param name="restoredBy">The identifier of the user or process that performed the restoration.</param>
    /// <returns>An <see cref="EntityDeletionStatus{TKey}"/> representing the result of the restoration operation.</returns>
    EntityDeletionStatus<TKey> Restore(TKey restoredBy);

    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="createdBy">The identifier of the user or process that performed the creation.</param>
    /// <returns>An <see cref="EntityCreationStatus{TKey}"/> representing the result of the creation operation.</returns>
    EntityCreationStatus<TKey> Create(TKey createdBy);

    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    /// <param name="deletedBy">The identifier of the user or process that performed the deletion.</param>
    /// <param name="deletedAt">The date and time when the entity was deleted.</param>
    /// <param name="isDeleted">Indicates whether the entity is marked as deleted.</param>
    /// <returns>An <see cref="EntityDeletionStatus{TKey}"/> representing the result of the deletion operation.</returns>
    EntityDeletionStatus<TKey> Delete(TKey deletedBy,
                                      bool isDeleted,
                                      DateTime? deletedAt = null);

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="modifiedBy">The identifier of the user or process that performed the update.</param>
    /// <returns>An <see cref="EntityModificationStatus{TKey}"/> representing the result of the update operation.</returns>
    EntityModificationStatus<TKey> Update(TKey modifiedBy);
}
