using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PromotionEngine.Models.BusinessLogic;

namespace PromotionEngine
{
    public class Program
    {
        static void Main(string[] args)
        {
            var hostBuilder = new HostBuilder();
            hostBuilder.UseEnvironment("Development")
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Startup>();
                ConfigureServices(services);
            }).ConfigureLogging(configureLogging =>
            {
                configureLogging.AddConsole();
                configureLogging.SetMinimumLevel(LogLevel.Information);
            });

            hostBuilder.RunConsoleAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPromotionEngineCheckoutProcess, PromotionEngineCheckoutProcess>();

            services.AddScoped<IPromotionTypes, PromotionTypes>();
        }
    }
}
