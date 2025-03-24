using Api.Constants;
using Application.Contracts;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[Route($"{Routes.BaseRoute.Name}")]
public sealed class GetAllTopicsEndpoint : EndpointBaseSync
                                           .WithoutRequest
                                           .WithActionResult<IEnumerable<string>>
{
    private IChannelManager Manager { get; }
    private ILogger<GetAllTopicsEndpoint> Logger { get; }

    public GetAllTopicsEndpoint(IChannelManager manager, ILogger<GetAllTopicsEndpoint> logger)
    {
        Manager = manager;
        Logger = logger;
    }

    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet($"{Routes.Topics.ReadAll}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override ActionResult<IEnumerable<string>> Handle()
    {
            Logger.LogInformation("Fetching all topics");

            IAsyncEnumerable<string> topics = Manager.GetActiveTopics();

            Logger.LogInformation("Successfully fetched all topics");

            return Ok(topics);
    }
}