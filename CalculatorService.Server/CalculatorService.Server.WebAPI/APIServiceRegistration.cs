using CalculatorService.Server.WebAPI.Controllers;
using System.Text.Json.Serialization;

namespace CalculatorService.Server.WebAPI
{
    public static class APIServiceRegistration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            return services;
        }
        public static WebApplication AddAPIApplications(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }


    }
}
