using Microsoft.AspNetCore.Mvc;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Controlller IsAlive
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class IsAliveController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<IsAliveController> _logger;
        /// <summary>
        /// Constructor with Serilog logger
        /// </summary>
        /// <param name="logger"></param>
        public IsAliveController(ILogger<IsAliveController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// When the user is sent to the server through the controller, the IsAlive may return Ok 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetResult()
        {
            try
            {
                _logger.LogInformation("I'm alive");
                return Ok("I'm alive");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed : {ex}");
                return BadRequest($"Failed");
            }
        }
    }
}
