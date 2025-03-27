using Application.Providers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Seed;

public static partial class Seed
{
    /// <summary>
    /// Seeds a client application into the database.
    /// </summary>
    /// <param name="app">The web application instance.</param>
    /// <param name="appName">The name of the client application to seed.</param>
    /// <param name="secret">The application secret</param>
    /// <param name="clientId">The unique identifier for the client application.</param>
    /// <param name="isDeleted">Indicates whether the client application is marked as deleted.</param>
    /// <param name="deletedAt">The date and time when the client application was deleted, if applicable.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private static async Task SeedClientApplicationAsync(WebApplication app,
                                                         string appName,
                                                         bool isDeleted,
                                                         DateTime? deletedAt = null)
    {
        using var scope = app.Services.CreateAsyncScope();
        ReadContext readContext = scope.ServiceProvider
                               .GetRequiredService<IDesignTimeDbContextFactory<ReadContext>>()
                               .CreateDbContext(null!);
        WriteContext writeContext = scope.ServiceProvider
                                .GetRequiredService<IDesignTimeDbContextFactory<WriteContext>>()
                                .CreateDbContext(null!);

        if (!readContext.Applications.Any(a => a.Name == appName))
        {
            var application = new ClientApplication
            {
                Id = Guid.NewGuid().ToString(),
                Name = appName,
                EntityCreationStatus = new EntityStatusProvider<string>().Create("SYSTEM"),
                EntityModificationStatus = new EntityStatusProvider<string>().Update("SYSTEM"),
                EntityDeletionStatus = new EntityStatusProvider<string>().Delete("SYSTEM", isDeleted, deletedAt),
            };

            writeContext.Applications.Add(application);
            await writeContext.SaveChangesAsync();
        }
    }
}