using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Models;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Add copy controlller 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CopyController : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<CopyController> _logger;
        /// <summary>
        /// Book model
        /// </summary>
        private readonly Models.Copy _model;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public CopyController(ILogger<CopyController> logger, Models.Copy model)
        {
            _logger = logger;
            _model = model;
        }
     
        [HttpGet("{id}")]
        public IActionResult GetCopyById(int id)
        {
            try
            {
                var copy = _model.GetCopyById(id);

                if (copy == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Getting copy by ID:{CopyID}", copy.ItemID);
                _logger.LogInformation("Copy received ");
                return Ok(_model.GetCopyByIdResponse(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Update book by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult UpdateCopy(int id, [FromBody] Domain.DTOs.Copy request)
        {
            try
            {
                var updatedCopy = _model.UpdateCopy(id, request);
                if (updatedCopy == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Updating copy by ID:{CopyID}", updatedCopy.ItemID);
                _logger.LogInformation("Copy updated ");

                return Ok(new Domain.DTOs.Copy
                {
                    ItemID = request.ItemID,
                    Name = request.Name,
                    Author = request.Author,
                    ISBN = request.ISBN,
                    Publisher = request.Publisher,
                    PublishingDate = request.PublishingDate
                });
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
        /// <summary>
        /// Delete book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteCopy(int id)
        {
            try
            {
                var copy = _model.GetCopyById(id);
                if (copy == null)
                {
                    return NotFound();
                }

                _model.DeleteCopy(id);
                _logger.LogInformation("Deleting copy by ID:{CopyID}", copy.CopyID);
                _logger.LogInformation("Copy deleted ");

                return NoContent();
            }

            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
    }
}
