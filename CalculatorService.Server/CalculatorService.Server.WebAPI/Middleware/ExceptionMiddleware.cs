using CalculatorService.Server.WebAPI.Middleware;
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
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                CodeErrorException codeErrorException = new(ex);
                string result = JsonConvert.SerializeObject(ex);

                context.Response.StatusCode = codeErrorException.ErrorStatus;
                await context.Response.WriteAsync(result);
            }
        }
    }

}
