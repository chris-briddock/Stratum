using Application.Contracts;

namespace Application.Extensions;
/// <summary>
/// Provides extension methods for applying specifications to queries. 
/// </summary>
public static class SpecificationExtensions
{
    /// <summary>
    /// Applies the specified specifications to the query.
    /// </summary>
    /// <typeparam name="T">The type of the entity to which the specifications are applied. </typeparam>
    /// <param name="query">The query to which the specifications are applied.</param>
    /// <param name="specifications">The specifications to apply to the query.</param>
    /// <returns>The <see cref="IQueryable{T}"> to which the specifications are applied.</returns>
    public static IQueryable<T> Apply<T>(this IQueryable<T> query,
                                         params ISpecification<T>[] specifications) 
    where T : class 
    {
        foreach (var spec in specifications)
            query = spec.Apply(query);

        return query;
    }
}