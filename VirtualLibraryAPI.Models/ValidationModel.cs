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
    /// Model for validation item
    /// </summary>
    public class ValidationModel : IValidationModel
    {
        /// <summary>
        /// Configuration for validation
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Using copy repository
        /// </summary>
        private readonly ICopyRepository _repository;
        /// <summary>
        /// Logger for validation
        /// </summary>
        private readonly ILogger<ValidationModel> _logger;
        /// <summary>
        /// Constructor with  Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public ValidationModel(ILogger<ValidationModel> logger,IConfiguration configuration, ICopyRepository repository)
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
        public ValidationStatus IsCopyValidForBooking(int copyId,int requestedBookingPeriod)
        {
            try
            {
                _logger.LogInformation($" Check if copy is valid  from Validation  model: CopyID {copyId} , Requested booking period {requestedBookingPeriod}");
                var copy = _repository.GetCopyById(copyId);
                if (copy == null)
                {
                    _logger.LogInformation($"Copy: {copyId} not found");
                    return ValidationStatus.NotFound;
                }
                if (!copy.IsAvailable)
                {
                    _logger.LogInformation($"Copy: {copyId} is  not available ");
                    return ValidationStatus.NotAvailable;
                }
                var maxPeriod = GetMaxDaysByItemType(copy.Type);
                if (requestedBookingPeriod > maxPeriod || requestedBookingPeriod < 1)
                {
                    _logger.LogInformation($"Copy: {copyId} booking period not suitable ");
                    return ValidationStatus.InvalidBookingPeriod;
                }
                return ValidationStatus.Valid;
            }
            catch (DbException ex)
            {
                _logger.LogError("An error occurred due to problems with database: {Error}", ex.Message );
                return ValidationStatus.DbError;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while processing the validation: {Error}", ex.Message);
                return ValidationStatus.InternalServerError;
            }
        }
        /// <summary>
        /// Get max days of copy by item type
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private int GetMaxDaysByItemType(Domain.DTOs.Type itemType)
        {
            _logger.LogInformation($" Check max days for item type from Validation  model:  {itemType}");
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
