using Application.Providers;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Factories;

/// <summary>
/// Factory for creating a new instance of <see cref="ClientApplication"/>.
/// </summary>
public static class ClientApplicationFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="ClientApplication"/>.
    /// </summary>
    /// <param name="name">The name of the client application.</param>
    /// <param name="apiKey">The API key of the client application.</param>
    /// <param name="description">The description of the client application. </param>
    /// <param name="user">
    /// The user who is creating the client application.</param>
    /// <returns>A new instance of <see cref="ClientApplication"/></returns>
    public static ClientApplication Create(string name,
                                           string apiKey,
                                           string description,
                                           string user)
    {
        return new ClientApplication
        {
            Name = name,
            ApiKey = apiKey,
            Description = description,
            EntityCreationStatus = new EntityStatusProvider<string>().Create(user),
            EntityModificationStatus = new EntityStatusProvider<string>().Update(user),
            EntityDeletionStatus = new EntityDeletionStatus<string>(false, null, null)
        };
    }
}