using System.Net;

namespace CalculatorService.Server.WebAPI.Middleware.ResponseException
{
    public abstract class ResponseBaseException
    {
        public ResponseBaseException(int statusCode, string message)
        {
            ErrorCode = "InternalError";
            ErrorStatus = statusCode;
            ErrorMessage = message;
        }
        public int ErrorStatus { get; }
        public string ErrorMessage { get; } = string.Empty;
        public string ErrorCode { get; } = string.Empty;
    }
}
