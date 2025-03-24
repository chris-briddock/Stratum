using System.Threading.Channels;
using Api.Constants;
using Application.Contracts;
using Ardalis.ApiEndpoints;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[Route($"{Routes.BaseRoute.Name}")]
public sealed class UnsubscribeEndpoint : EndpointBaseAsync
                                          .WithRequest<UnsubscribeRequest>
                                          .WithActionResult
{
    public IChannelManager Manager { get; }
    public ISubscriber<object> Subscriber { get; }
    public ILogger<UnsubscribeEndpoint> Logger { get; }

    public UnsubscribeEndpoint(IChannelManager manager,
                               ILogger<UnsubscribeEndpoint> logger,
                               ISubscriber<object> subscriber)
    {
        Manager = manager;
        Logger = logger;
        Subscriber = subscriber;
    }
    [HttpPost($"{Routes.Subscribers.Unsubscribe}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public override async Task<ActionResult> HandleAsync(UnsubscribeRequest request,
                                                   CancellationToken cancellationToken = default)
    {
            Channel<object> channel = Manager.GetOrCreateTopicChannel<object>(request.TopicName);

            if (channel is null)
            {
                Logger.LogWarning("Unsubscribe request failed: Channel for topic '{TopicName}' does not exist.", request.TopicName);
                return NotFound($"Channel for topic '{request.TopicName}' does not exist.");
            }
            await Subscriber.UnsubscribeAsync(channel, cancellationToken);

            Logger.LogInformation("Successfully unsubscribed from topic '{TopicName}'.", request.TopicName);
            return Ok($"Successfully unsubscribed from topic '{request.TopicName}'.");
    }
}