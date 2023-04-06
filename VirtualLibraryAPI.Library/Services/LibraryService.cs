using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.ServiceProcess;
using VirtualLibraryAPI.Domain;

namespace VirtualLibraryAPI.Library.Services
{
    /// <summary>
    /// Represents the Windows service that runs the web application.
    /// </summary>
    public class LibraryService : ServiceBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<LibraryService> _logger;
        /// <summary>
        /// Host
        /// </summary>
        private readonly IHost _host;
        /// <summary>
        /// Cancellation Token Source
        /// </summary>
        private readonly CancellationTokenSource _stopHostToken;
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationContext _dbContext;

        /// <summary>
        /// Settings for library service with host 
        /// </summary>
        /// <param name="host"></param>
        public LibraryService(IHost host)
        {
            ILoggerFactory? loggerFactory = (ILoggerFactory?)host.Services.GetService(typeof(ILoggerFactory));
            _logger = loggerFactory?.CreateLogger<LibraryService>() ?? NullLogger<LibraryService>.Instance;
            _host = host;
            _stopHostToken = new CancellationTokenSource();
            _dbContext = _host.Services.GetRequiredService<ApplicationContext>();

        }

        /// <summary>
        /// Starts the Windows service.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            _logger.LogInformation("Library service is starting");

            try
            {
                _dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database migration failed");
            }

            Task.Run(async () =>
            {
                try
                {
                    await _host.RunAsync(_stopHostToken.Token);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, "Host builder init is failed");
                }
            });

            _logger.LogInformation("Library service is started");
        }


        /// <summary>
        /// Stops the Windows service
        /// </summary>
        protected override void OnStop()
        {
            _stopHostToken.Cancel();
            base.OnStop();

            _logger.LogInformation("Library service is  stopped");
        }
    }
}
