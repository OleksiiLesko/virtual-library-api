using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Domain.Models;
using VirtualLibraryAPI.Domain;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Add item controlller 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AddItemController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<IsAliveController> _logger;
        /// <summary>
        /// Constructor with Serilog logger
        /// </summary>
        /// <param name="logger"></param>
        public AddItemController(ILogger<IsAliveController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
     //   public IActionResult Item(
     //[FromForm] string type,
     //[FromForm] string name,
     //[FromForm] DateOnly publishingDate,
     //[FromForm] string publisher,
     //[FromForm] string author,
     //[FromForm] string isbn,
     //[FromForm] float version,
     //[FromForm] string magazineName,
     //[FromForm] string magazinesIssueNumber,
     //[FromForm] int issueNumber)
     //   {
     //       try
     //       {
     //       }
     //       catch (ArgumentException ex)
     //       {
     //           _logger.LogError(ex.Message);
     //           return BadRequest("Invalid input.");
     //       }
     //   }
    }
}
