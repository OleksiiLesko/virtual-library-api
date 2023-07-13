using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Models;
using VirtualLibraryAPI.Repository;
using UserType = VirtualLibraryAPI.Common.UserType;

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
        /// Validation copy model
        /// </summary>
        private readonly IValidationCopyModel _validationCopyModel;
        /// <summary>
        /// Validation user model
        /// </summary>
        private readonly IValidationUserModel _validationUserModel;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public ManagementController(ILogger<ManagementController> logger, IManagementModel model, IValidationCopyModel validationCopyModel, IValidationUserModel validationUserModel)
        {
            _logger = logger;
            _managementModel = model;
            _validationCopyModel = validationCopyModel;
            _validationUserModel = validationUserModel;
        }
        /// <summary>
        /// Reserve copy by id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        [HttpPost("Copy/{copyId}/Booking")]
        public IActionResult ReserveCopyById( [FromHeader] int userId, int copyId, int bookingPeriod)
        {
            try
            {
                ///1.StringEnumConverter
                ///2.userid from header
                ///3.Info table user
                ///4.In AddUser UserType
                ///5. In CanUserReserveCopy check if its admin
                ///6.Tests for AddUser
                ///7.JsonProperty Type in Copy and UserType in User Dto
                var validationUserResult = _validationUserModel.CanUserReserveCopy(userId);

                if (validationUserResult == ValidationUserStatus.Valid)
                {
                    var validationCopyResult = _validationCopyModel.IsCopyValidForBooking(copyId, bookingPeriod);
                    if (validationCopyResult == ValidationCopyStatus.Valid)
                    {
                        var copy = _managementModel.ReserveCopyById(userId, copyId, bookingPeriod);
                        _logger.LogInformation("Booking copy by ID: {CopyID}", copy.CopyID);
                        _logger.LogInformation("Copy booked.");
                        var expirationDate = copy.ExpirationDate;

                        return Ok(new Domain.DTOs.BookingCopy
                        {
                            UserID = userId,
                            CopyID = copyId,
                            ExpirationDate = expirationDate,
                        });
                    }
                    else
                    {
                        return HandleCopyNotValidResult(validationCopyResult, copyId);
                    }
                }
                else
                {
                    return HandleUserNotValidResult(validationUserResult, copyId);
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
        private IActionResult HandleCopyNotValidResult(ValidationCopyStatus validationResult,int copyId)
        {
            switch (validationResult)
            {
                case ValidationCopyStatus.NotFound:
                    return NotFound($"CopyID: {copyId}  not found");

                case ValidationCopyStatus.DbError:
                    return StatusCode(500, "Database error");

                case ValidationCopyStatus.InternalServerError:
                    return StatusCode(500, "Internal server error");

                case ValidationCopyStatus.InvalidBookingPeriod:
                    return BadRequest($"Invalid booking period in CopyID {copyId}");

                case ValidationCopyStatus.NotAvailable:
                    return Conflict($"CopyID {copyId} not available for booking");

                default:
                    return BadRequest($"Invalid validation status of CopyID {copyId}");
            }
        }
        /// <summary>
        /// Return validation status of user
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        private IActionResult HandleUserNotValidResult(ValidationUserStatus validationResult, int userId)
        {
            switch (validationResult)
            {
                case ValidationUserStatus.UserNotFound:
                    return NotFound($"UserID: {userId}  not found");

                case ValidationUserStatus.DbError:
                    return StatusCode(500, "Database error");

                case ValidationUserStatus.InternalServerError:
                    return StatusCode(500, "Internal server error");

                case ValidationUserStatus.MaxCopiesExceeded:
                    return BadRequest($"Max count of copies exceeded UserID {userId}");

                case ValidationUserStatus.ExpiredCopy:
                    return BadRequest($"You have a copies that has expired UserID {userId}");

                default:
                    return BadRequest($"Invalid validation status of UserID {userId}");
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
        [HttpGet("Items/BookingExpired")]
        public IActionResult GetAllExpiredItems()
        {
            try
            {
                var item = _managementModel.GetAllExpiredItems();
                if (item == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Return all items with expired booking ");
                return Ok(_managementModel.GetAllExpiredItemsResponse());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the request: {Error}", ex.Message);
                return BadRequest($"Failed");
            }
        }
    }
}
