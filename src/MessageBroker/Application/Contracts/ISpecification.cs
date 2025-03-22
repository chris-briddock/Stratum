namespace Application.Contracts;

public interface ISpecification<T> where T : class
{
    IQueryable<T> Apply(IQueryable<T> query);
}