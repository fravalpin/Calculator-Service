using System.Net;

namespace CalculatorService.Server.WebAPI.Middleware.ResponseException.ResponseException
{
    public class ResponseValidationException : ResponseBaseException
    {
        public ResponseValidationException(FluentValidation.ValidationException ex) : base((int)HttpStatusCode.BadRequest,
            "Unable to process request: " + ex.Message)
        {
        }
    }
}
