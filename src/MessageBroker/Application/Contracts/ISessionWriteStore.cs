using Domain.Entities;

namespace Application.Contracts;

public interface ISessionWriteStore
{
    Task<Session> CreateAsync(Session session);
    Task DeleteAsync(Session session);
}
