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
    public class Copy : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<Copy> _logger;
        /// <summary>
        /// Book model
        /// </summary>
        private readonly Models.Copy _model;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public Copy(ILogger<Copy> logger, Models.Copy model)
        {
            _logger = logger;
            _model = model;
        }
        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public IActionResult GetAllCopies()
        //{
        //    try
        //    {
        //        _logger.LogInformation("Get all copies ");
        //        var books = _model.GetAllCopies();
        //        _logger.LogInformation("Copies received ");
        //        return Ok(_model.GetAllBooksResponse());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
        //        return BadRequest($"Failed");
        //    }
        //}
        /// <summary>
        /// Add Book
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public IActionResult AddCopy([FromBody] Domain.DTOs.Copy request)
        //{
        //    try
        //    {
        //        var addedCopy = _model.AddCopy(request);
        //        _logger.LogInformation("Adding copy:{CopyID}", addedCopy.ItemID);

        //        _logger.LogInformation("Copy added");
        //        return Ok(new Domain.DTOs.Copy
        //        {
        //            ItemID = request.ItemID,
        //            Name = request.Name,
        //            PublishingDate = (DateTime)request.PublishingDate,
        //            Publisher = request.Publisher,
        //            ISBN = request.ISBN,
        //            Author = request.Author
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
        //        return BadRequest($"Failed");
        //    }
        //}
        /// <summary>
        ///  Get book by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
