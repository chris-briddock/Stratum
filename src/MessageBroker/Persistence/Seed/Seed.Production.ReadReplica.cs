using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Seed;

/// <summary>
/// Provides methods for seeding databases within the application.
/// </summary>
public static partial class Seed
{
    /// <summary>
    /// Asynchronously ensures that the read replica database is created and sets up replication from the primary database.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to access the application's services.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method performs the following steps:
    /// <list type="number">
    /// <item>
    /// <description>Creates a scoped service provider to access application services.</description>
    /// </item>
    /// <item>
    /// <description>Retrieves the <see cref="IDesignTimeDbContextFactory{ReadContext}"/> service to create a <see cref="ReadContext"/> instance.</description>
    /// </item>
    /// <item>
    /// <description>Ensures that the read replica database is created.</description>
    /// </item>
    /// <item>
    /// <description>Executes a SQL script to set up replication from the primary database to the read replica.</description>
    /// </item>
    /// </list>
    /// The SQL script performs the following actions:
    /// <list type="bullet">
    /// <item>
    /// <description>Checks if the 'MessageBroker-Read' database exists.</description>
    /// </item>
    /// <item>
    /// <description>If it does not exist, restores the database from the primary replica and sets up replication.</description>
    /// </item>
    /// <item>
    /// <description>If it exists, logs that no changes were made.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public static async Task SeedReadReplica(WebApplication app)
    {
        using var scope = app.Services.CreateAsyncScope();

        var service = scope.ServiceProvider.GetRequiredService<IDesignTimeDbContextFactory<ReadContext>>();

        ReadContext dbContext = service.CreateDbContext(null!);

        await dbContext.Database.EnsureCreatedAsync();

        string? sqlScript = """
        -- Enable database replication for the read replica
        IF NOT EXISTS (
            SELECT 1 
            FROM sys.databases 
            WHERE name = 'MessageBroker-Read'
        )
        BEGIN
            -- Restore the database from the primary replica to the read replica
            RESTORE DATABASE [MessageBroker-Read] 
            FROM DISK = N'/var/opt/mssql/data/MessageBroker-Write.bak'
            WITH 
                MOVE 'MessageBroker-Write_Data' TO '/var/opt/mssql/data/MessageBroker-Read_Data.mdf',
                MOVE 'MessageBroker-Write_Log' TO '/var/opt/mssql/data/MessageBroker-Read_Log.ldf',
                NORECOVERY;

            -- Set up log shipping or transactional replication
            EXEC sp_addsubscription 
                @publication = N'MessageBrokerPublication',
                @subscriber = N'ReadReplicaServer',
                @destination_db = N'MessageBroker-Read',
                @subscription_type = N'Push',
                @sync_type = N'Automatic',
                @article = N'all';

            PRINT 'Replication from write database to read database activated successfully.';
        END
        ELSE
        BEGIN
            PRINT 'Read replica already exists. No changes made.';
        END;
        """;

        await dbContext.Database.ExecuteSqlRawAsync(sqlScript);
    }
}