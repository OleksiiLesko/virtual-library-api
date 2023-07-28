using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Library.Middleware;
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
        /// Validation issuer model
        /// </summary>
        private readonly IValidationIssuerModel _validationIssuerModel;
        /// <summary>
        /// Validation copy model
        /// </summary>
        private readonly IValidationCopyModel _validationCopyModel;
        /// <summary>
        /// Validation user model
        /// </summary>
        private readonly IValidationClientModel _validationClientModel;
        /// <summary>
        /// Constructor with logger,context and model
        /// </summary>
        /// <param name="logger"></param>
        public ManagementController(ILogger<ManagementController> logger, IManagementModel model, IValidationCopyModel validationCopyModel, IValidationClientModel validationClientModel, IValidationIssuerModel validationIssuerModel)
        {
            _logger = logger;
            _managementModel = model;
            _validationCopyModel = validationCopyModel;
            _validationClientModel = validationClientModel;
            _validationIssuerModel = validationIssuerModel;
        }
        /// <summary>
        /// Reserve copy by id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        [HttpPost("Copy/{copyId}/Booking")]
        [MiddlewareFilter(typeof(IssuerAuthenticationMiddleware))]
        public IActionResult ReserveCopyById([FromHeader] int issuerId, int clientId, int copyId, int bookingPeriod)
        {
            try
            {
                var validationIssuerResult = _validationIssuerModel.CanIssuerIssueCopy(issuerId, copyId);
                if (validationIssuerResult != ValidationIssuerStatus.Valid)
                {
                    return HandleIssuerNotValidResult(validationIssuerResult, issuerId, copyId);
                }

                var validationUserResult = _validationClientModel.CanClientReserveCopy(clientId);
                if (validationUserResult != ValidationUserStatus.Valid)
                {
                    return HandleUserNotValidResult(validationUserResult, clientId);
                }

                var validationCopyResult = _validationCopyModel.IsCopyValidForBooking(copyId, bookingPeriod);
                if (validationCopyResult != ValidationCopyStatus.Valid)
                {
                    return HandleCopyNotValidResult(validationCopyResult, copyId);
                }

                var copy = _managementModel.ReserveCopyById(clientId, copyId, bookingPeriod);
                _logger.LogInformation("Booking copy by ID: {CopyID}", copy.CopyID);
                _logger.LogInformation("Copy booked.");
                var expirationDate = copy.ExpirationDate;

                return Ok(new Domain.DTOs.BookingCopy
                {
                    ClientID = clientId,
                    CopyID = copyId,
                    ExpirationDate = expirationDate,
                });
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
        private IActionResult HandleUserNotValidResult(ValidationUserStatus validationResult, int clientId)
        {
            switch (validationResult)
            {
                case ValidationUserStatus.UserNotFound:
                    return NotFound($"UserID: {clientId}  not found");

                case ValidationUserStatus.DbError:
                    return StatusCode(500, "Database error");

                case ValidationUserStatus.InternalServerError:
                    return StatusCode(500, "Internal server error");

                case ValidationUserStatus.MaxCopiesExceeded:
                    return BadRequest($"Max count of copies exceeded UserID {clientId}");

                case ValidationUserStatus.ExpiredCopy:
                    return BadRequest($"You have a copies that has expired UserID {clientId}");

                default:
                    return BadRequest($"Invalid validation status of UserID {clientId}");
            }
        }
        /// <summary>
        /// Return validation status of issuer
        /// </summary>
        /// <param name="validationResult"></param>
        /// <returns></returns>
        private IActionResult HandleIssuerNotValidResult(ValidationIssuerStatus validationResult, int issuerId,int copyId)
        {
            switch (validationResult)
            {
                case ValidationIssuerStatus.UserNotFound:
                    return NotFound($"UserID: {issuerId}  not found");

                case ValidationIssuerStatus.CopyNotFound:
                    return NotFound($"CopyID: {copyId}  not found");

                case ValidationIssuerStatus.DbError:
                    return StatusCode(500, "Database error");

                case ValidationIssuerStatus.InternalServerError:
                    return StatusCode(500, "Internal server error");

                case ValidationIssuerStatus.UserDepartmentNotEqualCopyDepartment:
                    return BadRequest($"IssuerID {issuerId} deaprtment not equal CopyID {copyId} deaprtment");

                default:
                    return BadRequest($"Invalid validation status of IssuerID {issuerId}");
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
