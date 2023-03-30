using System.ServiceProcess;
using VirtualLibraryAPI.Library.Services;

namespace VirtualLibraryAPI.Library
{
    /// <summary>
    /// Class for run project as service
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Run library service 
        /// </summary>
        /// <param name="host"></param>
        public static void RunAsService(this IHost host)
        {
            var hostService = new LibraryService(host);
            ServiceBase.Run(hostService);
        }
    }
}
