using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.DTOs
{
    /// <summary>
    /// BookingCopy DTO
    /// </summary>
    public class BookingCopy
    {
        /// <summary>
        /// Id of copy
        /// </summary>
        public int CopyID { get; set; }
        /// <summary>
        /// Expiration date of copy
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }
}
