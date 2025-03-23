using Application.Results;
using Domain.Entities;

namespace Application.Contracts;

/// <summary>
/// Defines a contract for managing write operations on client applications.
/// </summary>
public interface IClientApplicationWriteStore
{
    /// <summary>
    /// Adds a new client application to the store.
    /// </summary>
    /// <param name="clientApplication">The client application to add.</param>
    /// <param name="ctx">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<ClientApplicationResult> AddAsync(ClientApplication clientApplication,
                                           CancellationToken ctx = default);

    /// <summary>
    /// Deletes a client application from the store by its name.
    /// </summary>
    /// <param name="clientName">The name of the client application to delete.</param>
    /// <param name="ctx">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<ClientApplicationResult> DeleteAsync(string clientName,
                                              CancellationToken ctx = default);

    /// <summary>
    /// Updates an existing client application in the store.
    /// </summary>
    /// <param name="clientApplication">The client application with updated information.</param>
    /// <param name="ctx">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<ClientApplicationResult> UpdateAsync(ClientApplication clientApplication, CancellationToken ctx = default);
}