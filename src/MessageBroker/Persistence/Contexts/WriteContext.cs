using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

/// <summary>
/// Represents a database context for write operations, 
/// inheriting from <see cref="BaseContext"/>.
/// </summary>
public class WriteContext : BaseContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WriteContext"/>
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public WriteContext(DbContextOptions<WriteContext> options) : base(options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteContext"/>
    /// </summary>
    public WriteContext() : base()
    {
    }
}
