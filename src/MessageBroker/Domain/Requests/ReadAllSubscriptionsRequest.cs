namespace Domain.Requests;

public sealed record ReadAllSubscriptionsRequest
{
    public int Page { get; init; }

    public int PageSize { get; init; }

}