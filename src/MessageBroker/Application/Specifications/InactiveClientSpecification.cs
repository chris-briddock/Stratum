using Domain.Entities;

namespace Application.Specifications;

/// <summary>
/// Specification to filter inactive clients
/// </summary>
public sealed class InactiveClientSpecification : Specification<ClientApplication>
{
    /// <inheritdoc/>
    public override IQueryable<ClientApplication> Apply(IQueryable<ClientApplication> query)
    {
        return query.Where(c => c.EntityDeletionStatus.IsDeleted == true);
    }
    /// <inheritdoc/>
   public override bool IsSatisfiedBy(ClientApplication entity)
    {
        return entity.EntityDeletionStatus.IsDeleted == false;
    }
}