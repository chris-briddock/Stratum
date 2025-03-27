
using Application.Stores;

namespace Application.Contracts;

/// <summary>
/// Defines methods for reading client application data from a persistent store, 
/// including retrieval by name and paginated listings of active and deleted client applications.
/// </summary>
public interface IClientApplicationReadStore
{
    /// <summary>
    /// Retrieves a client application by its unique name.
    /// </summary>
    /// <param name="name">The name of the client application to be retrieved.</param>
    /// <param name="ctx">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the client application data,
    /// or null if no application with the specified name exists.
    /// </returns>
    Task<ClientApplicationDto<string>> GetClientByName(string name,
                                                       CancellationToken ctx = default);

    /// <summary>
    /// Retrieves a paginated list of active client applications.
    /// </summary>
    /// <param name="page">The page number to retrieve, starting from 1.</param>
    /// <param name="pageSize">The number of client applications to include per page.</param>
    /// <param name="ctx">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a paginated list of client applications.
    /// </returns>
    Task<List<ClientApplicationDto<string>>> GetClientsAsync(int page = 1,
                                                             int pageSize = 10,
                                                             CancellationToken ctx = default);

    /// <summary>
    /// Retrieves a paginated list of deleted client applications.
    /// </summary>
    /// <param name="page">The page number to retrieve, starting from 1.</param>
    /// <param name="pageSize">The number of deleted client applications to include per page.</param>
    /// <param name="ctx">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a paginated list of deleted client applications.
    /// </returns>
    Task<List<ClientApplicationDto<string>>> GetDeletedClients(int page = 1,
                                                               int pageSize = 10,
                                                               CancellationToken ctx = default);
}

