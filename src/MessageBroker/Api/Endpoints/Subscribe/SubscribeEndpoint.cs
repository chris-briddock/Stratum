using System.Threading.Channels;
using Api.Constants;
using Application.Contracts;
using Ardalis.ApiEndpoints;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[Route($"{Routes.BaseRoute.Name}")]
public sealed class SubscribeEndpoint : EndpointBaseAsync
                                        .WithRequest<SubscribeRequest>
                                        .WithActionResult
{
    private IChannelManager Manager { get; }
    private ISubscriber<object> Subscriber { get; }

    private ISubscriptionWriteStore WriteStore { get; }

    public SubscribeEndpoint(IChannelManager manager,
                             ISubscriber<object> subscriber,
                             ISubscriptionWriteStore writeStore)
    {
        Manager = manager;
        Subscriber = subscriber;
        WriteStore = writeStore;
    }
    
    [HttpPost($"{Routes.Subscribers.Subscribe}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult> HandleAsync(SubscribeRequest request,
                                                         CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Topic))
            return BadRequest("Topic cannot be null or empty.");

        Channel<object> channel = Manager.GetOrCreateTopicChannel<object>(request.Topic);

        Response.ContentType = "text/event-stream";
        Response.Headers.Add("Cache-Control", "no-cache");
        Response.Headers.Add("Connection", "keep-alive");

        await foreach (var item in Subscriber.SubscribeAsync<object>(channel, cancellationToken))
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            await Response.WriteAsync($"data: {item}\n\n", cancellationToken);
            await Response.Body.FlushAsync(cancellationToken);
        }

        return Ok();
    }
}
