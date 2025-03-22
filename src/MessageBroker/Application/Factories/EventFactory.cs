using Application.Providers;
using Domain.Entities;

namespace Application.Factories;

public static class EventFactory
{
    public static Event Create(string Type, string Payload)
    {
        return new Event()
        {
            Type = Type,
            Payload = Payload,
            EntityCreationStatus = new EntityStatusProvider<string>().Create("SYSTEM"),
        };
    }
}