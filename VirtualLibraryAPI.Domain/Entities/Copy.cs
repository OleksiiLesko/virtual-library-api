

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of Copies
    /// </summary>
    public class Copy 
    {
        /// <summary>
        ///  ID of item
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        ///  ID of client
        /// </summary>
        public int? ClientID { get; set; }
        /// <summary>
        /// Connect with Item
        /// </summary>
        public virtual Item Item { get; set; }
        /// <summary>
        /// Connect with Client
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// ID of copy
        /// </summary>
        public int CopyID { get; set; }
        /// <summary>
        /// Type of  copy
        /// </summary>
        public virtual Type Type { get; set; } = Type.Copy;
        /// <summary>
        /// Status of copy
        /// </summary>
        public bool IsAvailable { get; set; }
        /// <summary>
        /// Period of copy booked
        /// </summary>
        public int BookingPeriod { get; set; }
        /// <summary>
        /// Expiration date of copy
        /// </summary> 
        public DateTime? ExpirationDate { get; set; }
        
    }
}
