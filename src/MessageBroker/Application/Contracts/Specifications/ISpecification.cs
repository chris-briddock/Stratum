namespace Application.Contracts;

/// <summary>
/// Represents a specification pattern interface that defines the rules for querying and validating entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the entity to which the specification applies. Must be a reference type.
/// </typeparam>
public interface ISpecification<T> where T : class
{
    /// <summary>
    /// Applies the specification to a given queryable collection of entities, transforming it based on the specification's rules.
    /// </summary>
    /// <param name="query">
    /// The queryable collection of entities to which the specification will be applied.
    /// </param>
    /// <returns>
    /// A transformed <see cref="IQueryable{T}"/> that adheres to the rules defined by the specification.
    /// </returns>
    IQueryable<T> Apply(IQueryable<T> query);

    /// <summary>
    /// Determines whether a given entity satisfies the conditions defined by the specification.
    /// </summary>
    /// <param name="entity">
    /// The entity to evaluate against the specification's conditions.
    /// </param>
    /// <returns>
    /// <c>true</c> if the entity satisfies the specification; otherwise, <c>false</c>.
    /// </returns>
    bool IsSatisfiedBy(T entity);
}