using Domain.Entities;

namespace Application.Contracts;

/// <summary>
/// Defines the contract for writing session data.
/// </summary>
public interface ISessionWriteStore
{
    /// <summary>
    /// Creates a new session in the store.
    /// </summary>
    /// <param name="session">The session to be created.</param>
    /// <returns>A task representing the asynchronous operation, with the created <see cref="Session"/> as the result.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="session"/> is null.</exception>
    Task<Session> CreateAsync(Session session);

    /// <summary>
    /// Deletes a session from the store.
    /// </summary>
    /// <param name="session">The session to be deleted.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="session"/> is null.</exception>
    Task DeleteAsync(Session session);
}

