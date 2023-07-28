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
    public class ValidationClientModel : IValidationClientModel
    {
        /// <summary>
        /// Configuration for validation user
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Using user repository
        /// </summary>
        private readonly IClientRepository _repository;
        /// <summary>
        /// Logger for validation
        /// </summary>
        private readonly ILogger<ValidationClientModel> _logger;
        /// <summary>
        /// Constructor with  Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public ValidationClientModel(ILogger<ValidationClientModel> logger, IClientRepository repository,IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _configuration = configuration;
        }
        /// <summary>
        /// Validation if user can reserve a copy
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ValidationUserStatus CanClientReserveCopy(int clientId)
        {
            try
            {
                _logger.LogInformation($"Checking if client can reserve a copy from ValidationClient model: ClientID {clientId}");

                var client = _repository.GetClientById(clientId);

                if (client == null)
                {
                    _logger.LogInformation($"ClientID: {clientId} not found");
                    return ValidationUserStatus.UserNotFound;
                }
                var clientCopiesCount = _repository.CountClientCopies(clientId);

                var maxCopies = GetMaxCountCopies();

                if (clientCopiesCount > maxCopies)
                {
                    _logger.LogInformation($"Client: {clientId} has reached the max number of copies");
                    return ValidationUserStatus.MaxCopiesExceeded;
                }

                var hasExpiredCopy = _repository.HasExpiredCopy(clientId);

                if (hasExpiredCopy)
                {
                    _logger.LogInformation($"Client: {clientId} has expired copies");
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
        /// Get max count of copies for client
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private int GetMaxCountCopies()
        {
            _logger.LogInformation($"Checking max count of copies for client from ValidationClient model");

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
