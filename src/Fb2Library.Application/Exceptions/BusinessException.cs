namespace Fb2Library.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public string? ErrorCode { get; }

        public BusinessException(string message, string? errorCode = null)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
