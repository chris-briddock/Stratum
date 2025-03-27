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
   /// <inheritdoc/>
    public EntityCreationStatus<TKey> Create(TKey createdBy) => 
    new(DateTime.UtcNow, createdBy);

    /// <inheritdoc/>
    public EntityModificationStatus<TKey> Update(TKey modifiedBy) => 
    new(DateTime.UtcNow, modifiedBy);

    /// <inheritdoc/>
    public EntityDeletionStatus<TKey> Delete(TKey deletedBy, bool isDeleted, DateTime? deletedAt = null) => 
    new(isDeleted, deletedAt, deletedBy);

    /// <inheritdoc/>
    public EntityDeletionStatus<TKey> Restore(TKey restoredBy) => 
    new(false, DateTime.UtcNow, restoredBy);
}