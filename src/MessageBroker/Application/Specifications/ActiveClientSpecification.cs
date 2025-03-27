using Application.Specifications;
using Domain.Entities;

public sealed class ActiveClientSpecification : Specification<ClientApplication>
{
    public override IQueryable<ClientApplication> Apply(IQueryable<ClientApplication> query)
    {
        return query.Where(c => c.EntityDeletionStatus.IsDeleted == false);
    }

   public override bool IsSatisfiedBy(ClientApplication entity)
    {
        return entity.EntityDeletionStatus.IsDeleted == false;
    }
}