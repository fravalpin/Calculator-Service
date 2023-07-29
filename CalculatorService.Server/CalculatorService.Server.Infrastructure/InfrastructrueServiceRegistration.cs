using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace CalculatorService.Server.Infrastructure
{
    public static class InfrastructrueServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ILoggingBuilder logging, ConfigureHostBuilder host)
        {
            logging.ClearProviders();
            host.UseNLog();

            return services;
        }
    }
}
