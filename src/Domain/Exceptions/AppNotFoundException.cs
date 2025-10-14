namespace Core.Exceptions;

public class AppNotFoundException : Exception
{
    public string ErrorCode { get; private set; } = "404";

    public AppNotFoundException()
        : base() { }

    public AppNotFoundException(string message, string errorCode = "")
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public AppNotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}
