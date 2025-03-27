using Application.Dtos;
using Domain.Entities;

namespace Application.Contracts;

/// <summary>
/// Defines the contract for reading session data.
/// </summary>
public interface ISessionReadStore
{
    /// <summary>
    /// Retrieves a list of sessions associated with the specified user ID.
    /// </summary>
    /// <param name="applicationId">The ID of the user whose sessions are to be retrieved.</param>
    /// <param name="cancellation">A token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a list of <see cref="SessionDto"/> objects as the result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the userId is null or empty.</exception>
    Task<List<SessionDto>> GetAsync(string applicationId,
                                    CancellationToken cancellation = default);

    /// <summary>
    /// Retrieves a session by its unique session ID.
    /// </summary>
    /// <param name="sessionId">The ID of the session to retrieve.</param>
    /// <param name="cancellation">A token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with the <see cref="Session"/> object if found, or null if not found.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the sessionId is null or empty.</exception>
    Task<Session?> GetByIdAsync(string sessionId, CancellationToken cancellation = default);
}

