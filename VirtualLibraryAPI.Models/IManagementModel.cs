using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Management model interface 
    /// </summary>
    public interface IManagementModel
    {
        /// <summary>
        /// Reserve copy by Id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookedPeriod"></param>
        /// <returns></returns>
        Domain.DTOs.Copy ReserveCopyById(int copyId, int bookingPeriod);
        /// <summary>
        /// Return copy by Id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookedPeriod"></param>
        /// <returns></returns>
        Domain.DTOs.Copy ReturnCopyById(int copyId);
        /// <summary>
        /// Return items whose not expired
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Item> GetAllExpiredItems();
        /// <summary>
        /// Get all expired items for response
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.DTOs.Copy> GetAllExpiredItemsResponse();
    }
}
