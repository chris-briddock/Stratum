// filepath: /home/chris/Projects/MessageBroker/src/MessageBroker/Domain/Responses/ReadAllSubscriptionsResponse.cs
using Application.Extensions;
using Domain.Entities;

namespace Domain.Responses;

/// <summary>
/// Represents the response for reading all subscriptions.
/// </summary>
public class ReadAllSubscriptionsResponse<T> where T : class
{
    /// <summary>
    /// Gets or sets the subscriptions.
    /// </summary>
    public T Subscriptions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; set; }
}