using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Model for management
    /// </summary>
    public class ManagementModel : IManagementModel
    {
        /// Using management repository
        /// </summary>
        private readonly IManagementRepository _repository;
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<ManagementModel> _logger;

        /// <summary>
        /// Constructor with book Repository and logger
        /// </summary>
        /// <param name="bookRepository"></param>
        /// <param name="logger"></param>
        public ManagementModel(ILogger<ManagementModel> logger, IManagementRepository repository )
        {
            _logger = logger;
            _repository = repository;
        }
        /// <summary>
        /// Reserve copy by id 
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy ReserveCopyById(int copyId, int bookingPeriod)
        {
            _logger.LogInformation($"Reserve copy by id by id for response  from Management  model: CopyID {copyId}");
            var result = _repository.ReserveCopyById(copyId, bookingPeriod);
            if (result == null)
            {
                return result;
            }
            result.IsAvailable = false;
            result.ExpirationDate = DateTime.Now.AddDays(bookingPeriod);
            return result;
        }
        /// <summary>
        /// Return copy by id 
        /// </summary>
        /// <param name="copyId"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy ReturnCopyById(int copyId)
        {
            _logger.LogInformation($"Return copy by id  from Management model: CopyID {copyId}");
            var result = _repository.ReturnCopyById(copyId);
            if (result == null)
            {
                return result;
            }
            result.IsAvailable = true;
            result.ExpirationDate = DateTime.MinValue;
 
            return result;
        }
    }
}
