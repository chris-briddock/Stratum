namespace Domain.Models;

/// <summary>
/// Represents an error with a specific code and message.
/// </summary>
/// <param name="Code">A unique identifier or code that categorizes the error.</param>
/// <param name="Message">A descriptive message providing details about the error.</param>
/// <remarks>
/// This record is immutable and provides a convenient way to encapsulate error information.
/// The <see cref="ToString"/> method is overridden to return a formatted string combining the code and message.
/// </remarks>
public record Error(string Code, string Message)
{
    /// <summary>
    /// Returns a formatted string combining the code and message.
    /// </summary>
    /// <returns>A string representation of the error.</returns>
    public override string ToString() => $"{Code}: {Message}";
}
