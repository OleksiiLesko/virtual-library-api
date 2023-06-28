using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Library.Controllers
{
    /// <summary>
    /// Management controlller 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ManagementController : ControllerBase
    {

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ManagementController> _logger;
        /// <summary>
        /// Management model
        /// </summary>
        private readonly IManagementModel _managementModel;
        /// <summary>
        /// Validation model
        /// </summary>
        private readonly IValidationModel _validationModel;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public ManagementController(ILogger<ManagementController> logger, IManagementModel model, IValidationModel validationModel)
        {
            _logger = logger;
            _managementModel = model;
            _validationModel = validationModel;
        }
        /// <summary>
        /// Reserve copy by id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        [HttpPost("Copy/{copyId}/Booking")]
        public IActionResult ReserveCopyById(int copyId, int bookingPeriod)
        {
            try
            {
                var validationResult = _validationModel.IsCopyValidForBooking(copyId, bookingPeriod);

                if (validationResult == ValidationStatus.Valid)
                {
                    var copy = _managementModel.ReserveCopyById(copyId, bookingPeriod);
                    _logger.LogInformation("Booking copy by ID: {CopyID}", copy.CopyID);
                    _logger.LogInformation("Copy booked.");
                    var expirationDate = copy.ExpirationDate;

                    return Ok(new Domain.DTOs.BookingCopy
                    {
                        CopyID = copyId,
                        ExpirationDate = expirationDate,
                    });
                }
                else
                {
                    return HandleNotValidResult(validationResult, copyId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest("Failed");
            }
        }

        /// <summary>
        /// Return validation status of copy
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        private IActionResult HandleNotValidResult(ValidationStatus validationResult,int copyId)
        {
            switch (validationResult)
            {
                case ValidationStatus.NotFound:
                    return NotFound($"CopyID: {copyId}  not found");

                case ValidationStatus.DbError:
                    return StatusCode(500, "Database error");

                case ValidationStatus.InternalServerError:
                    return StatusCode(500, "Internal server error");

                case ValidationStatus.InvalidBookingPeriod:
                    return BadRequest($"Invalid booking period in CopyID {copyId}");

                case ValidationStatus.NotAvailable:
                    return Conflict($"CopyID {copyId} not available for booking");

                default:
                    return BadRequest($"Invalid validation status of CopyID {copyId}");
            }
        }
        /// <summary>
        /// Return copy by id
        /// </summary>
        /// <param name="copyId"></param>
        /// <returns></returns>
        [HttpPost("Copy/{copyId}/Return")]
        public IActionResult ReturnCopyById(int copyId)
        {
            try
            {
                var copy = _managementModel.ReturnCopyById(copyId);
                if (copy == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Return copy by ID:{CopyID}", copy.CopyID);
                _logger.LogInformation("Copy  returned ");
                return Ok("Copy returned");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
    }
}
