namespace Application.Constants;

/// <summary>
/// A static class that holds cache tag constants for tag-based cache eviction.
/// </summary>
public static class CacheTagConstants
{
    /// <summary>
    /// The cache tag for sessions.
    /// </summary>
    public const string Sessions = "sessions";

    /// <summary>
    /// The cache tag for activities.
    /// </summary>
    public const string Activities = "activities";

    public const string DeletedClients = "deleted_clients";

    public const string ClientByName = "client_by_name";
    public const string Clients = "clients";

    public const string Events = "events";
}