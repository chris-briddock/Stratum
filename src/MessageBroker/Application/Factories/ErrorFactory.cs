using Domain.Models;

namespace Application.Factories;

/// <summary>
/// Factory class for creating <see cref="Error"> objects.
/// </summary>
public static class ErrorFactory
{
    /// <summary>
    /// Creates a new <see cref="Error"/> object for an operation that has been cancelled.
    /// </summary>
    /// <param name="message">The message to include in the error. </param>
    /// <returns>A new <see cref="Error"/> object with the code "C01" and the specified message.</returns>
    public static Error OperationCancelled(string message) => 
        new("C01", $"The operation has been cancelled. {message}");
    /// <summary>
    /// Creates a new <see cref="Error"/> object for when a database update exception occurs.
    /// </summary>
    /// <param name="message">The message to include in the error.</param>
    /// <returns>A new <see cref="Error"/> object with the code "EF01" and the specified message.</returns>
    public static Error DbUpdateException(string message) =>
        new("EF01", $"Database update execption occurred: {message}");

    /// <summary>
    /// Creates a new <see cref="Error"/> object for when a database concurrency exception occurs.
    /// </summary>
    /// <param name="message">The message to include in the error.</param>
    /// <returns>A new <see cref="Error"/> object with the code "EF02" and the specified message.</returns>
    public static Error DbUpdateConcurrencyException(string message) =>
        new("EF02", $"Database concurrency exception occurred: {message}");
}