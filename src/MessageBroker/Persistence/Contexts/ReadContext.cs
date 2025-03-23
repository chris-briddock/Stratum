using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

/// <summary>
/// Represents a read-only database context derived from <see cref="BaseContext"/>.
/// </summary>
public class ReadContext : BaseContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReadContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the database context.</param>
    public ReadContext(DbContextOptions<ReadContext> options) : base(options) 
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadContext"/> class.
    /// </summary>
    public ReadContext() : base()
    {
    }

    /// <summary>
    /// Overrides the <see cref="DbContext.SaveChanges"/> method to prevent modifications, enforcing read-only behavior.
    /// </summary>
    /// <returns>This method does not return a value as it always throws an exception.</returns>
    /// <exception cref="InvalidOperationException">Thrown to indicate that the context is read-only.</exception>
    public override int SaveChanges()
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    /// <summary>
    /// Overrides the <see cref="DbContext.SaveChanges(bool)"/> method to prevent modifications, enforcing read-only behavior.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Indicates whether to accept all changes on success.</param>
    /// <returns>This method does not return a value as it always throws an exception.</returns>
    /// <exception cref="InvalidOperationException">Thrown to indicate that the context is read-only.</exception>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    /// <summary>
    /// Overrides the <see cref="DbContext.SaveChangesAsync(CancellationToken)"/> method to prevent asynchronous modifications, enforcing read-only behavior.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>This method does not return a value as it always throws an exception.</returns>
    /// <exception cref="InvalidOperationException">Thrown to indicate that the context is read-only.</exception>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }

    /// <summary>
    /// Overrides the <see cref="DbContext.SaveChangesAsync(bool, CancellationToken)"/> method to prevent asynchronous modifications, enforcing read-only behavior.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">Indicates whether to accept all changes on success.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>This method does not return a value as it always throws an exception.</returns>
    /// <exception cref="InvalidOperationException">Thrown to indicate that the context is read-only.</exception>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new InvalidOperationException("This context is read-only.");
    }
}