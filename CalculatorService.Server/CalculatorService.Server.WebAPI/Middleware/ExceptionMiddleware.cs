using CalculatorService.Server.WebAPI.Middleware.ResponseException;
using CalculatorService.Server.WebAPI.Middleware.ResponseException.ResponseException;
using Newtonsoft.Json;
namespace CalculatorService.Server.WebAPI.Controllers
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                ResponseValidationException codeErrorException = new(ex);
                await WriteJsonError(context, codeErrorException);
            }
            catch (Exception)
            {
                ResponseGeneralException codeErrorException = new();
                await WriteJsonError(context, codeErrorException);
            }
        }

        private static async Task WriteJsonError(HttpContext context, ResponseBaseException codeErrorException)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = codeErrorException.ErrorStatus;
            string result = JsonConvert.SerializeObject(codeErrorException);
            await context.Response.WriteAsync(result);
        }
    }

} 

