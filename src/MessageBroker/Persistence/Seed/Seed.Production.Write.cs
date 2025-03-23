using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Seed;

/// <summary>
/// Provides methods for seeding the database with initial data.
/// </summary>
public static partial class Seed
{
    /// <summary>
    /// Asynchronously ensures that the write database is created and ready for use.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to access the application's services.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task SeedWriteDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();
        var service = scope.ServiceProvider.GetRequiredService<IDesignTimeDbContextFactory<WriteContext>>();
        WriteContext dbContext = service.CreateDbContext(null!);
        await dbContext.Database.EnsureCreatedAsync();
    }
}