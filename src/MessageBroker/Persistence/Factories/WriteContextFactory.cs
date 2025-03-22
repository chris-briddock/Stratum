using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence.Factories;

public class WriteContextFactory : IDesignTimeDbContextFactory<WriteContext>
{
    public IConfiguration Configuration { get; }

    public WriteContextFactory(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public WriteContextFactory()
    {
    }

    public WriteContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WriteContext>();
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));

        return new WriteContext(optionsBuilder.Options, Configuration);
    }
}