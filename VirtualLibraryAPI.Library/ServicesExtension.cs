using System.ServiceProcess;
using VirtualLibraryAPI.Library.Services;

namespace VirtualLibraryAPI.Library
{
    public static class ServiceExtensions
    {
        public static void RunAsService(this IHost host)
        {
            var hostService = new LibraryService(host);
            ServiceBase.Run(hostService);
        }
    }
}
