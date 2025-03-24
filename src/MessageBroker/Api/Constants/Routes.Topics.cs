namespace Api.Constants;

/// <summary>
/// Defines routes related to topic management.
/// </summary>
public static partial class Routes
{
    public static class Topics
    {
        /// <summary>
        /// The route for creating a new topic.
        /// </summary>
        public const string Create = "topics";

        /// <summary>
        /// The route for retrieving all topics.
        /// </summary>
        public const string ReadAll = "topics";

        /// <summary>
        /// The route for retrieving topics by name.
        /// </summary>
        public const string Read = "topics/{name}";

        /// <summary>
        /// The route for deleting a specific topic.
        /// </summary>
        public const string Delete = "topics/{name}";
    }
}