using Serilog;
using Microsoft.Extensions.Configuration;

namespace CustomerService.Logging
{
    public static class SerilogConfig
    {
        public static void ConfigureLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/customer_service.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}