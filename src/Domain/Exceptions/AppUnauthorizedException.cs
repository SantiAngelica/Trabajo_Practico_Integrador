namespace Core.Exceptions;

public class AppUnauthorizedException : Exception
{
    public string ErrorCode { get; private set; } = "404";

    public AppUnauthorizedException()
        : base() { }

    public AppUnauthorizedException(string message, string errorCode = "")
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public AppUnauthorizedException(string message, Exception innerException)
        : base(message, innerException) { }
}