using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Factories;

/// <summary>
/// Factory for creating instances of <see cref="ReadContext"/> at design time.
/// </summary>
public class ReadContextFactory : IDesignTimeDbContextFactory<ReadContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadContextFactory"/> class.
    /// </summary>
    public ReadContextFactory()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="ReadContext"/> based on the provided arguments.
    /// </summary>
    /// <param name="args">Arguments passed by the design-time tools; not used in this implementation.</param>
    /// <returns>A new instance of <see cref="ReadContext"/> configured with the application's connection string.</returns>
    public ReadContext CreateDbContext(string[] args)
    {
        // Build the configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // Create options builder for ReadContext
        var optionsBuilder = new DbContextOptionsBuilder<ReadContext>();

        // Configure the context to use SQL Server with retry on failure
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("ReadConnection"), opt =>
        {
            opt.EnableRetryOnFailure();
        });

        // Return a new instance of ReadContext with the configured options
        return new ReadContext(optionsBuilder.Options);
    }
}