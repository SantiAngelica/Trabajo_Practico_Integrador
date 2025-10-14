namespace Core.Exceptions;

public class AppValidationException : Exception
{
    public string ErrorCode { get; private set; } = "401";

    public AppValidationException()
        : base() { }

    public AppValidationException(string message, string errorCode = "")
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public AppValidationException(string message, Exception innerException)
        : base(message, innerException) { }
}
