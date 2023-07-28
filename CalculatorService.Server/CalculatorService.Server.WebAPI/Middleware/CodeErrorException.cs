using System.Net;

namespace CalculatorService.Server.WebAPI.Middleware
{
    public class CodeErrorException
    {
        public CodeErrorException(FluentValidation.ValidationException ex) : this((int)HttpStatusCode.BadRequest,
            "Unable to process request:" + ex.Message)
        {
        }
        public CodeErrorException(Exception _) : this((int)HttpStatusCode.InternalServerError,
            "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support.")
        {
        }

        private CodeErrorException(int statusCode, string message)
        {
            ErrorCode = "InternalError";
            ErrorStatus = statusCode;
            ErrorMessage = message;
        }

        public int ErrorStatus { get; }
        public string ErrorMessage { get; }
        public string ErrorCode { get; }
    }
}
