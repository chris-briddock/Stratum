namespace Application.Constants;

/// <summary>
/// A static class that holds cache tag constants for tag-based cache eviction.
/// These constants represent various tags that can be used to identify specific cache entries
/// for invalidation when data is modified or deleted.
/// </summary>
public static class CacheTagConstants
{
    /// <summary>
    /// The cache tag for sessions.
    /// This tag is used for caching session-related data.
    /// </summary>
    public const string Sessions = "sessions";

    /// <summary>
    /// The cache tag for activities.
    /// This tag is used for caching activity-related data.
    /// </summary>
    public const string Activities = "activities";

    /// <summary>
    /// The cache tag for deleted clients.
    /// This tag is used for caching data related to clients that have been deleted.
    /// </summary>
    public const string DeletedClients = "deleted_clients";

    /// <summary>
    /// The cache tag for client data by name.
    /// This tag is used for caching client data identified by the client's name.
    /// </summary>
    public const string ClientByName = "client_by_name";

    /// <summary>
    /// The cache tag for clients.
    /// This tag is used for caching general client-related data.
    /// </summary>
    public const string Clients = "clients";

    /// <summary>
    /// The cache tag for events.
    /// This tag is used for caching event-related data.
    /// </summary>
    public const string Events = "events";
}