using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public sealed class WriteContext : BaseContext
{
    public WriteContext(DbContextOptions options,
                        IConfiguration configuration) : base(options, configuration) 
    {
    }

    public WriteContext()
    {
        
    }
    
}