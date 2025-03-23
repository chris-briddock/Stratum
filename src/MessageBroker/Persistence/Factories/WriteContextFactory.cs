using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Factories;

/// <summary>
/// Factory for creating instances of <see cref="WriteContext"/> at design time.
/// </summary>
public class WriteContextFactory : IDesignTimeDbContextFactory<WriteContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WriteContextFactory"/> class.
    /// </summary>
    public WriteContextFactory()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="WriteContext"/> based on the provided arguments.
    /// </summary>
    /// <param name="args">Arguments passed by the design-time tools; not used in this implementation.</param>
    /// <returns>A new instance of <see cref="WriteContext"/> configured with the application's connection string.</returns>
    public WriteContext CreateDbContext(string[] args)
    {
        // Build the configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // Create options builder for WriteContext
        var optionsBuilder = new DbContextOptionsBuilder<WriteContext>();

        // Configure the context to use SQL Server with retry on failure
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("WriteConnection"), opt =>
        {
            opt.EnableRetryOnFailure();
        });

        // Return a new instance of WriteContext with the configured options
        return new WriteContext(optionsBuilder.Options);
    }
}