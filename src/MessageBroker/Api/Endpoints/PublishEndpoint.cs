using System.Text.Json;
using System.Threading.Channels;
using Api.Constants;
using Application.Contracts;
using Application.Providers;
using Ardalis.ApiEndpoints;
using Domain.Entities;
using Domain.Requests;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[Route($"{Routes.BaseRoute.Name}")]
public sealed class PublishEndpoint : EndpointBaseAsync
                                      .WithRequest<PublishRequest>
                                      .WithActionResult
{
    public IChannelManager Manager { get; }
    public IPublisher Publisher { get; }
    public IEventWriteStore WriteStore { get; }

    public PublishEndpoint(IChannelManager manager,
                           IPublisher publisher,
                           IEventWriteStore writeStore)
    {
        Manager = manager;
        Publisher = publisher;
        WriteStore = writeStore;
    }

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost($"{Routes.Publish}")]
    public override async Task<ActionResult> HandleAsync(PublishRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        Channel<object> channel = Manager.GetOrCreateTopicChannel<object>(request.Topic, 1000);

        await Publisher.PublishAsync(request.Topic, request.Message, channel);

        Event @event = new()
        {
            Type = request.EventType,
            Payload = JsonSerializer.Serialize(request.Message),
            EntityCreationStatus = new EntityStatusProvider<string>().Create("SYSTEM")
        };
        var result = await WriteStore.AddEventAsync(@event, cancellationToken);

        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return StatusCode(StatusCodes.Status201Created);
    }
}

