using Domain.Models;

namespace Application.Factories;

public static class ErrorFactory
{
    public static Error OperationCancelled(string message) => 
        new("C01", $"The operation has been cancelled. {message}");

    public static Error DbUpdateException(string message) =>
        new("EF01", $"Database update execption occurred: {message}");


    public static Error DbUpdateConcurrencyException(string message) =>
        new("EF02", $"Database concurrency exception occurred");
}