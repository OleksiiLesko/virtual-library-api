using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.DTOs;
using VirtualLibraryAPI.Domain.Entities;
using VirtualLibraryAPI.Repository;
using VirtualLibraryAPI.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VirtualLibraryAPI.Repository.Repositories;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for validation copy
    /// </summary>
    public class ValidationCopyModel : IValidationCopyModel
    {
        /// <summary>
        /// Configuration for validation copy
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Using copy repository
        /// </summary>
        private readonly ICopyRepository _repository;
        /// <summary>
        /// Logger for validation
        /// </summary>
        private readonly ILogger<ValidationCopyModel> _logger;
        /// <summary>
        /// Constructor with  Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public ValidationCopyModel(ILogger<ValidationCopyModel> logger,IConfiguration configuration, ICopyRepository repository)
        {
            _logger = logger;
            _configuration = configuration;
            _repository = repository;
        }
        /// <summary>
        /// Check if copy is valid  for booking
        /// </summary>
        /// <param name="copyId"></param>
        /// <returns></returns>
        public ValidationCopyStatus IsCopyValidForBooking(int copyId,int requestedBookingPeriod)
        {
            try
            {
                _logger.LogInformation($" Check if copy is valid  from Validation  model: CopyID {copyId} , Requested booking period {requestedBookingPeriod}");
                var copy = _repository.GetCopyById(copyId);
                if (copy == null)
                {
                    _logger.LogInformation($"Copy: {copyId} not found");
                    return ValidationCopyStatus.NotFound;
                }
                if (!copy.IsAvailable)
                {
                    _logger.LogInformation($"Copy: {copyId} is  not available ");
                    return ValidationCopyStatus.NotAvailable;
                }
                var maxPeriod = GetMaxDaysByItemType(copy.Type);
                if (requestedBookingPeriod > maxPeriod || requestedBookingPeriod < 1)
                {
                    _logger.LogInformation($"Copy: {copyId} booking period not suitable ");
                    return ValidationCopyStatus.InvalidBookingPeriod;
                }
                return ValidationCopyStatus.Valid;
            }
            catch (DbException ex)
            {
                _logger.LogError("An error occurred due to problems with database: {Error}", ex.Message );
                return ValidationCopyStatus.DbError;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the validation: {Error}", ex.Message);
                return ValidationCopyStatus.InternalServerError;
            }
        }
        /// <summary>
        /// Get max days of copy by item type
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private int GetMaxDaysByItemType(Domain.DTOs.Type itemType)
        {
            _logger.LogInformation($" Check max days for item type from ValidationCopy  model:  {itemType}");
            int maxDays;
            switch (itemType)
            {
                case Domain.DTOs.Type.Book:
                    if (!int.TryParse(_configuration["MaxDays:Book"], out maxDays))
                    {
                        _logger.LogInformation($"Book not converted from config to int ");
                        return 0;
                    }
                    break;
                case Domain.DTOs.Type.Magazine:
                    if (!int.TryParse(_configuration["MaxDays:Magazine"], out maxDays))
                    {
                        _logger.LogInformation($"Magazine not converted from config to int ");
                        return 0;
                    }
                    break;
                case Domain.DTOs.Type.Article:
                    if (!int.TryParse(_configuration["MaxDays:Article"], out maxDays))
                    {
                        _logger.LogInformation($"Article not converted from config to int ");
                        return 0;
                    }
                    break;
                default:
                    return 0;
            }

            return maxDays;
        }
    }
}
