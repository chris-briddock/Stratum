namespace Persistence.Seed;

/// <summary>
/// Provides methods for seeding applications within the production environment.
/// </summary>
public static partial class Seed
{
    public static async Task SeedDefaultApplication(WebApplication app) => 
    await SeedClientApplicationAsync(app, "Default Application", false);
}