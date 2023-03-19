using Microsoft.AspNetCore;

namespace BirthdayCalculator.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        IWebHost host = CreateWebHostBuilder(args).Build();
        await host.RunAsync();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args).UseKestrel()
          .ConfigureAppConfiguration((hostingContext, config) =>
          {
              config
                  .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                  .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
          })
          .ConfigureLogging((hostingContext, logging) =>
          {
              logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
              logging.AddConsole();
              logging.AddDebug();
              logging.AddEventSourceLogger();
          })
          .UseStartup<Startup>();
}
