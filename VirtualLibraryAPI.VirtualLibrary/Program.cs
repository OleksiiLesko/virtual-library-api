using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Extensions.Logging;
using VirtualLibraryAPI.Library.Controllers;

namespace VirtualLibraryAPI.Library
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application starting!");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context,services) =>
                {

                })
                .UseSerilog()
                .Build();
        }
        /// <summary>
        /// Manual connection with appsettings.json
        /// </summary>
        /// <param name="builder"></param>
        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}