using Application.Providers;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Factories;

public static class ClientApplicationFactory
{
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