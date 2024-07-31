namespace Authenticator.Domain.Validation.GlobalExceptionHandlers.Exceptions;

/// <summary>
/// Global BadRequestException
/// </summary>
public class BadRequestException : Exception
{
    public BadRequestException() { }
    public BadRequestException(string message) : base(message) { }
    public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
}