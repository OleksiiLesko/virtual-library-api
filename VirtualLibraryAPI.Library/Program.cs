using Serilog;
using VirtualLibraryAPI.Library;
using Host = Microsoft.Extensions.Hosting.Host;

namespace VirtualLibraryAPI.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogger();

            try
            {
                Log.Information("Starting up...");

                IHost host = CreateHostBuilder(args).Build();

                if (args.Contains("--run-as-service"))
                {
                    Log.Information("Running as a service");
                    host.RunAsService();
                }
                else
                {
                    Log.Information("Running as a web server");
                    host.Run();
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        /// <summary>
        /// Configure Logger
        /// </summary>
        private static void ConfigureLogger()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        /// <summary>
        /// Create host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
        }
    }
}