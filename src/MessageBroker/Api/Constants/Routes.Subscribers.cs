using Nest;

namespace Api.Constants;

/// <summary>
/// Defines routes related to subscription management.
/// </summary>
public static partial class Routes
{
    public static class Subscribers
    {
        /// <summary>
        /// The route for subscribing to a topic.
        /// </summary>
        public const string Subscribe = "subscribe";

        /// <summary>
        /// The route for unsubscribing from a specific topic.
        /// </summary>
        public const string Unsubscribe = "subscriptions/{name}";

        /// <summary>
        /// The route for retrieving all active subscriptions.
        /// </summary>
        public const string ReadAll = "subscriptions";
        /// <summary>
        /// The route for retrieving a specific subscription.
        /// </summary>
        public const string ReadByName = "subscriptions/{name}";
        /// <summary>
        /// The route for retrieving all subscriptions by application.
        /// </summary>
        public const string ReadByAppication = "subscriptions/applications/{name}";
    }
}