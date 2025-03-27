namespace Api.Constants;

public static partial class Routes
{
    /// <summary>
    /// Defines routes related to application management.
    /// </summary>
    public static class Applications
    {
        /// <summary>
        /// The route for retrieving all applications.
        /// </summary>
        public const string ReadAll = "applications";
        /// <summary>
        /// The route for retrieving a specific application.
        /// </summary>
        public const string Read = "applications/{name}";
        /// <summary>
        /// The route for creating a new application.
        /// </summary>
        public const string Create = "applications";
        /// <summary>
        /// The route for updating an existing application.
        /// </summary>
        public const string Update = "applications/{name}";
        /// <summary>
        /// The route for deleting an existing application.
        /// </summary>
        public const string Delete = "applications/{name}";
    }
}