using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.DTOs;
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
        /// Return all items with expired booking
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetAllExpiredItems()
        {
            _logger.LogInformation("Return all items with expired booking");
            var items = _repository.GetAllExpiredItems();
            if (items.Any())
            {
                return items;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Return all items with expired booking for response
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Copy> GetAllExpiredItemsResponse()
        {
            _logger.LogInformation("Get all expired items for response DTO from Management model  ");
            var result = _repository.GetAllExpiredItemsResponse();
            if (result == null)
            {
                return result;
            }
            return result;
        }

        /// <summary>
        /// Reserve copy by id 
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookingPeriod"></param>
        /// <returns></returns>
        public Domain.DTOs.Copy ReserveCopyById(int userId, int copyId, int bookingPeriod)
        {
            _logger.LogInformation($"Reserve copy  by id for response  from Management  model: CopyID {copyId}");
            var result = _repository.ReserveCopyById(userId,copyId, bookingPeriod);
            if (result == null)
            {
                return result;
            }
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
            return result;
        }
    }
}
