namespace Api.Constants;

public static partial class Routes
{
    /// <summary>
    /// Defines routes related to message management.
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// The route for retrieving all messages.
        /// </summary>
        public const string Read = "events";

        /// <summary>
        /// The route for retrieving a specific message.
        /// </summary>
        public const string ReadByName = "events/{name}";

        /// <summary>
        /// The route for creating a new message.
        /// </summary>
        public const string Create = "events";
    }
}