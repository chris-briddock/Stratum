using Application.Contracts;

namespace Application.Specifications;

/// <summary>
/// Base class for all specifications
/// </summary>
/// <typeparam name="T">The type of entity that the specification is for </typeparam>
public abstract class Specification<T> : ISpecification<T> where T : class
{
    /// <summary>
    /// Apply the specification to the query
    /// </summary>
    /// <param name="query">The query to apply the specification to</param>
    /// <returns>The query with the specification applied.</returns>
    public abstract IQueryable<T> Apply(IQueryable<T> query);

    /// <summary>
    /// Check if the specification is satisfied by the entity
    /// </summary>
    /// <param name="entity">The entity to check if the specification is satisfied by</param>
    /// <returns>True if the specification is satisfied by the entity, false otherwise</returns>
    public abstract bool IsSatisfiedBy(T entity);
}