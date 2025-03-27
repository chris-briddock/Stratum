using System.Text.Json;
using System.Threading.Channels;
using Api.Constants;
using Application.Contracts;
using Application.Providers;
using Ardalis.ApiEndpoints;
using Domain.Entities;
using Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[Route($"{Routes.BaseRoute.Name}")]
public sealed class PublishEndpoint : EndpointBaseAsync
                                      .WithRequest<PublishRequest>
                                      .WithActionResult
{
    private IChannelManager Manager { get; }
    private IPublisher Publisher { get; }
    private IEventWriteStore WriteStore { get; }
    private ILogger<PublishEndpoint> Logger { get; }

    public PublishEndpoint(IChannelManager manager,
                           IPublisher publisher,
                           IEventWriteStore writeStore,
                           ILogger<PublishEndpoint> logger)
    {
        Manager = manager;
        Publisher = publisher;
        WriteStore = writeStore;
        Logger = logger;
    }

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost($"{Routes.Publisher.Publish}")]
    public override async Task<ActionResult> HandleAsync(PublishRequest request,
                                                        CancellationToken cancellationToken = default)
    {
        Channel<object> channel = Manager.GetOrCreateTopicChannel<object>(request.Topic);

        Logger.LogInformation("Publishing message to topic {Topic}", request.Topic);

        await Publisher.PublishAsync(request.Topic, request.Message, channel, cancellationToken);

        Logger.LogInformation("Message published to topic {Topic}", request.Topic);

        Event @event = new()
        {
            Type = request.EventType,
            Payload = JsonSerializer.Serialize(request.Message),
            EntityCreationStatus = new EntityStatusProvider<string>().Create("SYSTEM")
        };

        Logger.LogInformation("Writing event to store");
        var result = await WriteStore.AddEventAsync(@event, cancellationToken);

        Logger.LogInformation("Event written to store");

        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError);
        
        return StatusCode(StatusCodes.Status201Created);
    }
}

