using Application.Providers;
using Domain.Entities;

namespace Application.Factories;

/// <summary>
/// Factory class for creating <see cref="Event"/> objects.
/// </summary>
public static class EventFactory
{
    /// <summary>
    /// Creates a new <see cref="Event"/> object.
    /// </summary>
    /// <param name="Type">The type of the event.</param>
    /// <param name="Payload">The payload of the event.</param>
    /// <returns>A new <see cref="Event"/> object with the specified type and payload.</returns>
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