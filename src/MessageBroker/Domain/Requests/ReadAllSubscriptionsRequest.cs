namespace Domain.Requests;

/// <summary>
/// Request to read all subscriptions
/// </summary>
public sealed record ReadAllSubscriptionsRequest
{
    /// <summary>
    /// Page number
    /// </summary>
    public int Page { get; init; }
    /// <summary>
    /// Page size
    /// </summary>
    public int PageSize { get; init; }

}