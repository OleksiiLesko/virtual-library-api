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
using FluentValidation;
using Copy = VirtualLibraryAPI.Domain.DTOs.Copy;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for validation copy
    /// </summary>
    public class ValidationCopyModel : AbstractValidator<Copy>
    {
        private readonly ILogger<ValidationCopyModel> _logger;
        private readonly ICopyRepository _repository;
        private readonly IConfiguration _configuration;

        public ValidationCopyModel( ICopyRepository repository, IConfiguration configuration, ILogger<ValidationCopyModel> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;

            RuleFor(model => model.CopyID).NotNull()
                .NotEmpty()
                .WithMessage($"Copy must not be null or empty.")
                .Must(BeValidIsAvailable)
                .WithMessage("Copy  not available.");

            RuleFor(model => model.BookingPeriod).NotNull()
                .NotEmpty()
                .WithMessage("BookingPeriod must not be null or empty.")
                .Must((model, requestedBookingPeriod) => BeValidBookingPeriod(requestedBookingPeriod, model.CopyID))
                .WithMessage("BookingPeriod is invalid for the specified CopyID.");
        }
        /// <summary>
        /// Check if copy is available 
        /// </summary>
        /// <param name="copyId"></param>
        /// <returns></returns>
        private bool BeValidIsAvailable(int copyId)
        {
            var copy = _repository.GetCopyById(copyId);
            return  copy.IsAvailable;
        }
        /// <summary>
        /// Check if booking period for copy valid
        /// </summary>
        /// <param name="requestedBookingPeriod"></param>
        /// <param name="copyId"></param>
        /// <returns></returns>
        private bool BeValidBookingPeriod(int requestedBookingPeriod, int copyId)
        {
            var copy = _repository.GetCopyById(copyId);
            var maxPeriod = GetMaxDaysByItemType(copy.Type);
            return requestedBookingPeriod >= 1 && requestedBookingPeriod <= maxPeriod;
        }
        /// <summary>
        /// Get max days of copy by item type
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        private int GetMaxDaysByItemType(Common.Type itemType)
        {
            int maxDays;
            switch (itemType)
            {
                case Common.Type.Book:
                    if (!int.TryParse(_configuration["MaxDays:Book"], out maxDays))
                    {
                        _logger.LogInformation($"Book not converted from config to int");
                        return 0;
                    }
                    break;
                case Common.Type.Magazine:
                    if (!int.TryParse(_configuration["MaxDays:Magazine"], out maxDays))
                    {
                        _logger.LogInformation($"Magazine not converted from config to int");
                        return 0;
                    }
                    break;
                case Common.Type.Article:
                    if (!int.TryParse(_configuration["MaxDays:Article"], out maxDays))
                    {
                        _logger.LogInformation($"Article not converted from config to int");
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
