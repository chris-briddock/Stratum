using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Factories;
public class ReadContextFactory : IDesignTimeDbContextFactory<ReadContext>
{

    public IConfiguration Configuration { get; }

    public IServiceProvider Services { get; }

    public ReadContextFactory(IConfiguration configuration, IServiceProvider services)
    {
        Configuration = configuration;
        Services = services;
    }

    public ReadContextFactory()
    {

    }

    public ReadContext CreateDbContext(string[] args)
    {

        var optionsBuilder = Services.GetRequiredService<DbContextOptionsBuilder>();

        return new ReadContext(optionsBuilder.Options, Configuration);
    }
}