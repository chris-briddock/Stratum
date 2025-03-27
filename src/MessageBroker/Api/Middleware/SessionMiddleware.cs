using System.Security.Claims;
using Application.Constants;
using Application.Contracts;
using Application.Extensions;
using Domain.Entities;

namespace Api.Middlware;
/// <summary>
/// Middleware to ensure each HTTP session has a unique session ID.
/// </summary>
public sealed class SessionMiddleware
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    /// <summary>
    /// Invokes the next middleware in the request pipeline.
    /// </summary>
    private RequestDelegate Next { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="SessionMiddleware"/>
    /// </summary>
    /// <param name="next">Invokes the next middleware in the request pipeline.</param>
    public SessionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    /// <summary>
    /// Processes an HTTP request to ensure it has a unique session ID.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>A task that represents the completion of request processing.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        string? emailAddress = context.User.FindFirst(ClaimTypes.Email)?.Value;
        bool shouldCreate = false;
        using AsyncServiceScope scope = context.RequestServices.CreateAsyncScope();
        try
        {
            await _semaphore.WaitAsync();
            string? currentSessionId = context.Session.GetString(SessionConstants.SequenceId);
            var sessionReadStore = scope.ServiceProvider.GetRequiredService<ISessionReadStore>();

            if (currentSessionId is null)
                shouldCreate = true;

            if (currentSessionId is not null)
            {
                var storedSession = await sessionReadStore.GetByIdAsync(currentSessionId);

                if (storedSession is null)
                    shouldCreate = true;

            }

            if (shouldCreate)
            {
                context.Session.SetString(SessionConstants.SequenceId, context.Session.Id);
                var sessionWriteStore = scope.ServiceProvider.GetRequiredService<ISessionWriteStore>();

                // Create a new Session object
                Session session = new()
                {
                    SessionId = context.Session.Id,
                    IpAddress = context.GetIpAddress(),
                    UserAgent = context.Request.Headers.UserAgent.ToString(),
                    Status = SessionStatus.Active,
                    EntityCreationStatus = new(DateTime.UtcNow, context.User?.Identity?.Name ?? "SYSTEM"),
                    EntityDeletionStatus = new(false, null, null)
                    
                };

                await sessionWriteStore.CreateAsync(session);
            }

        }
        finally
        {
            _semaphore.Release();
        }

        await Next(context);
    }
}