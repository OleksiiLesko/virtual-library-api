using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Repository;
using UserType = VirtualLibraryAPI.Common.UserType;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for validation issuer
    /// </summary>
    public class ValidationIssuerModel : IValidationIssuerModel
    {
        /// <summary>
        /// Using copy repository
        /// </summary>
        private readonly ICopyRepository _copyRepository;
        /// <summary>
        /// Using user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Logger for validation
        /// </summary>
        private readonly ILogger<ValidationIssuerModel> _logger;
        /// <summary>
        /// Constructor with  Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public ValidationIssuerModel(ILogger<ValidationIssuerModel> logger, IUserRepository userRepository,ICopyRepository copyRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _copyRepository = copyRepository;
        }
        /// <summary>
        /// Validation if issuer issue a copy from  his department
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ValidationIssuerStatus CanIssuerIssueCopy(int useriId,int copyId)
        {
            try
            {
                _logger.LogInformation($"Checking if issuer can issue a copy from ValidationIssuer model: ClientID {useriId}");

                var user = _userRepository.GetUserById(useriId);

                if (user == null)
                {
                    _logger.LogInformation($"UserID: {useriId} not found");
                    return ValidationIssuerStatus.UserNotFound;
                }

                var copy = _copyRepository.GetCopyById(copyId);
                if (copy == null)
                {
                    _logger.LogInformation($"ItemID: {copyId} not found");
                    return ValidationIssuerStatus.CopyNotFound;
                }
                if (user.UserType == UserType.Manager)
                {
                    _logger.LogInformation($"Issuer type {user.UserType} not a manager ");
                    return ValidationIssuerStatus.Valid;
                }
                if (user.UserType == UserType.Administrator)
                {
                    if (user.DepartmentID != copy.DepartmentID)
                    {
                        _logger.LogInformation($"ItemID: {copy.ItemID} not equal UserID: {useriId} for reserve copy ");
                        return ValidationIssuerStatus.UserDepartmentNotEqualCopyDepartment;
                    }
                }


                return ValidationIssuerStatus.Valid;
            }
            catch (DbException ex)
            {
                _logger.LogError("An error occurred due to problems with the database: {Error}", ex.Message);
                return ValidationIssuerStatus.DbError;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the validation: {Error}", ex.Message);
                return ValidationIssuerStatus.InternalServerError;
            }
        }
    }
}
