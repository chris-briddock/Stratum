using Domain.Entities;

namespace Application.Specifications;

public sealed class InactiveClientSpecification : Specification<ClientApplication>
{
    public override IQueryable<ClientApplication> Apply(IQueryable<ClientApplication> query)
    {
        return query.Where(c => c.EntityDeletionStatus.IsDeleted == true);
    }

   public override bool IsSatisfiedBy(ClientApplication entity)
    {
        return entity.EntityDeletionStatus.IsDeleted == false;
    }
}