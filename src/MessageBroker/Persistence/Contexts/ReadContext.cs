using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;
public sealed class ReadContext : BaseContext
{
    public ReadContext(DbContextOptions options,
                       IConfiguration configuration) : base(options, configuration) 
    {
    }

    public ReadContext()
    {
    }

    
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    

}