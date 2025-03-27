using Api.Constants;
using Application.Contracts;
using Application.Dtos;
using Ardalis.ApiEndpoints;
using Domain.Requests;
using Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[Route($"{Routes.BaseRoute.Name}")]
public sealed class ReadAllSubscriptions : EndpointBaseAsync
                                           .WithRequest<ReadAllSubscriptionsRequest>
                                           .WithActionResult
{

    public ISubscriptionReadStore ReadStore { get; }

    public ReadAllSubscriptions(ISubscriptionReadStore readStore)
    {
        ReadStore = readStore;
    }

    [HttpGet(Routes.Subscribers.ReadAll)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync(ReadAllSubscriptionsRequest request,
                                                         CancellationToken cancellationToken = default)
    {
        var subscriptions = await ReadStore.GetSubscriptionsAsync(request.Page, request.PageSize, cancellationToken);

        if (subscriptions is null)
            return NotFound();

        var response = new ReadAllSubscriptionsResponse<List<SubscriptionDto>>()
        {
            Subscriptions = subscriptions,
            IsSuccess = true,
        };
        return Ok(response);
    }
}