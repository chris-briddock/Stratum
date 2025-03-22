using Domain.ValueObjects;

namespace Application.Stores;

/// <summary>
/// Represents a client application and its associated metadata, including identification, 
/// security credentials, description, and status information regarding creation, modification, and deletion.
/// </summary>
/// <typeparam name="TKey">The type of the unique identifier used to distinguish client applications.</typeparam>
public sealed class ClientApplicationDto<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// The name of the client application, which serves as a human-readable identifier.
    /// Typically used for display and management purposes.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// The API key assigned to the client application, used for authentication and authorization.
    /// It serves as a credential allowing secure access to protected resources.
    /// </summary>
    public string ApiKey { get; set; } = default!;

    /// <summary>
    /// A brief description providing contextual information about the client application.
    /// This may include details about its purpose, usage scenarios, or configuration.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// An object representing the creation status of the client application.
    /// Includes information about when the application was created and by whom.
    /// </summary>
    public EntityCreationStatus<TKey> EntityCreationStatus { get; set; } = default!;

    /// <summary>
    /// An object containing metadata about modifications made to the client application.
    /// Tracks changes to the application, including timestamps and responsible users.
    /// </summary>
    public EntityModificationStatus<TKey> EntityModificationStatus { get; set; } = default!;

    /// <summary>
    /// An object detailing the deletion status of the client application.
    /// Captures information about whether the application has been marked for deletion or is no longer active.
    /// </summary>
    public EntityDeletionStatus<TKey> EntityDeletionStatus { get; set; } = default!;
}