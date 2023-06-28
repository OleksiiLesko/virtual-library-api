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
    /// Validation model interface 
    /// </summary>
    public interface IValidationModel
    {
        /// <summary>
        /// Check if copy is valid for booking
        /// </summary>
        /// <param name="copyId"></param>
        /// <param name="requestedBookingPeriod"></param>
        /// <returns></returns>
        public ValidationStatus IsCopyValidForBooking(int copyId, int requestedBookingPeriod);
    }
}
