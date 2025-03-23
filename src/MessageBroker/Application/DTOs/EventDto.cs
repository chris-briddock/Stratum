using Domain.ValueObjects;

namespace Application.DTOs;

/// <summary>
/// Represents a data transfer object for an event, encapsulating its type,
/// payload, and creation status information.
/// </summary>
/// <typeparam name="TKey">
/// The type of the unique identifier for the entity, which must implement
/// <see cref="IEquatable{TKey}"/>.
/// </typeparam>
public sealed class EventDto<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Specifies the nature or category of the event.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// Contains the data associated with the event in serialized format.
    /// </summary>
    public string Payload { get; set; } = default!;

    /// <summary>
    /// Provides metadata about the creation of the event, including the creator's
    /// identifier and the timestamp of creation.
    /// </summary>
    public EntityCreationStatus<TKey> EntityCreationStatus { get; set; } = default!;
}