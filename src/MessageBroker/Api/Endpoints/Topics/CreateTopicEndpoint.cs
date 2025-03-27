using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public sealed class CreateTopicEndpoint : EndpointBaseAsync
                                          .WithRequest<CreateTopicRequest>
                                          .WithActionResult
{
    public override async Task<ActionResult> HandleAsync(CreateTopicRequest request,
                                                   CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(Ok());    
    }
}

public class CreateTopicRequest
{
}