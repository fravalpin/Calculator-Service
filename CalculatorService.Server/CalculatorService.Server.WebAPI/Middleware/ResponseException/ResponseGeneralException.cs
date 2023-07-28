using System.Net;

namespace CalculatorService.Server.WebAPI.Middleware.ResponseException.ResponseException
{
    public class ResponseGeneralException : ResponseBaseException
    {
        public ResponseGeneralException() : base((int)HttpStatusCode.InternalServerError,
            "An unexpected error condition was triggered which made impossible to fulfill the request. Please try again or contact support.")
        {
        }

    }
}
