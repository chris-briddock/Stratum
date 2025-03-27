using Application.Specifications;
using Domain.Entities;

/// <summary>
/// Specification to filter active clients
/// </summary>
public sealed class ActiveClientSpecification : Specification<ClientApplication>
{
    /// <inheritdoc/>
    public override IQueryable<ClientApplication> Apply(IQueryable<ClientApplication> query)
    {
        return query.Where(c => c.EntityDeletionStatus.IsDeleted == false);
    }

    /// <inheritdoc/>
   public override bool IsSatisfiedBy(ClientApplication entity)
    {
        return entity.EntityDeletionStatus.IsDeleted == false;
    }
}