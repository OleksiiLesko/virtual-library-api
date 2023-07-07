using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for validation user
    /// </summary>
    public class ValidationUserModel : IValidationUserModel
    {
        /// <summary>
        /// Configuration for validation user
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Using user repository
        /// </summary>
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Logger for validation
        /// </summary>
        private readonly ILogger<ValidationUserModel> _logger;
        /// <summary>
        /// Constructor with  Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public ValidationUserModel(ILogger<ValidationUserModel> logger, IUserRepository userRepository,IConfiguration configuration)
        {
            _logger = logger;
            _userRepository = userRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// Validation if user can reserve a copy
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ValidationUserStatus CanUserReserveCopy(int userId)
        {
            try
            {
                _logger.LogInformation($"Checking if user can reserve a copy from ValidationUser model: UserID {userId}");

                var user = _userRepository.GetUserById(userId);

                if (user == null)
                {
                    _logger.LogInformation($"UserID: {userId} not found");
                    return ValidationUserStatus.UserNotFound;
                }

                var userCopiesCount = _userRepository.CountUserCopies(userId);

                var maxCopies = GetMaxCountCopies();

                if (userCopiesCount > maxCopies)
                {
                    _logger.LogInformation($"User: {userId} has reached the max number of copies");
                    return ValidationUserStatus.MaxCopiesExceeded;
                }

                var hasExpiredCopy = _userRepository.HasExpiredCopy(userId);

                if (hasExpiredCopy)
                {
                    _logger.LogInformation($"User: {userId} has expired copies");
                    return ValidationUserStatus.ExpiredCopy;
                }

                return ValidationUserStatus.Valid;
            }
            catch (DbException ex)
            {
                _logger.LogError("An error occurred due to problems with the database: {Error}", ex.Message);
                return ValidationUserStatus.DbError;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the validation: {Error}", ex.Message);
                return ValidationUserStatus.InternalServerError;
            }
        }
        /// <summary>
        /// Get max count of copies for user
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private int GetMaxCountCopies()
        {
            _logger.LogInformation($"Checking max count of copies for user from ValidationUser model");

            int maxCopies;
            if (!int.TryParse(_configuration["MaxCopies:Copy"], out maxCopies))
            {
                _logger.LogInformation($"Copy  not converted from config to int");
                return 0;
            }

            return maxCopies;
        }
    }
}
