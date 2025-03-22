using Application.DTOs;
using Domain.Entities;

namespace Application.Contracts;

public interface ISessionReadStore
{
    Task<List<SessionDto>> GetAsync(string userId, CancellationToken cancellation = default);
    Task<Session?> GetByIdAsync(string sessionId, CancellationToken cancellation = default);
}
