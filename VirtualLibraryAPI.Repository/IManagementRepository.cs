using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Repository
{
    /// <summary>
    /// Management repository interface 
    /// </summary>
    public interface IManagementRepository
    {
        /// <summary>
        /// Reserve copy by Id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookedPeriod"></param>
        /// <returns></returns>
        Domain.DTOs.Copy ReserveCopyById(int copyId,int bookingPeriod);
        /// <summary>
        /// Return copy by Id
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="bookedPeriod"></param>
        /// <returns></returns>
        Domain.DTOs.Copy ReturnCopyById(int copyId);
    }
}
