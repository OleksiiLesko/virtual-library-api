﻿using Microsoft.Extensions.Logging.Abstractions;
using System.ServiceProcess;

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
        /// Settings for library service with host 
        /// </summary>
        /// <param name="host"></param>
        public LibraryService(IHost host)
        {
            ILoggerFactory? loggerFactory = (ILoggerFactory?)host.Services.GetService(typeof(ILoggerFactory));
            _logger = loggerFactory?.CreateLogger<LibraryService>() ?? NullLogger<LibraryService>.Instance;
            _host = host;
            _stopHostToken = new CancellationTokenSource();
            
        }

        /// <summary>
        /// Starts the Windows service.
        /// </summary>
        protected override void OnStart(string[] args)
        {
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

            base.OnStart(args);

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
