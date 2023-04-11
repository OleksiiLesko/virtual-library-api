using Microsoft.Data.SqlClient;
using Serilog;

namespace VirtualLibraryAPI.Library
{
    public static class DatabaseUtils
    {
        /// <summary>
        /// Create database connection string
        /// </summary>
        /// <returns></returns>
        public static string CreateConnectionString(IConfiguration configuration)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = configuration.GetSection("DbHostName")["Server"];
            builder.InitialCatalog = configuration.GetSection("DbHostName")["Database"];
            builder.TrustServerCertificate = true;
            builder.IntegratedSecurity = true;
            builder.ConnectRetryCount = 1;

            return builder.ConnectionString;
        }
        /// <summary>
        /// Configure Logger
        /// </summary>
        public static void ConfigureLogger()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
