using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;

namespace VirtualLibraryAPI.Models
{
    /// <summary>
    /// Validation copy model interface 
    /// </summary>
    public interface IValidationCopyModel
    {
        /// <summary>
        /// Check if copy is valid for booking
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="requestedBookingPeriod"></param>
        /// <returns></returns>
        public ValidationCopyStatus IsCopyValidForBooking(int copyId, int requestedBookingPeriod);
    }
}
