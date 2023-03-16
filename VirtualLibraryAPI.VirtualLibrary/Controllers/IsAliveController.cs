using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class IsAliveController 
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger<IsAliveController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public IsAliveController(ILogger<IsAliveController> logger)
        {
            _logger = logger;
        }
    }
}
