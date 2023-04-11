namespace VirtualLibraryAPI.Domain.Models
{
    /// <summary>
    /// Table of Copies
    /// </summary>
    public class Copies : Items
    {
        /// <summary>
        /// 
        /// </summary>
        public Items? Item { get; set; }
        /// <summary>
        /// ID of copy
        /// </summary>
        public int CopyID { get; set; }
    }
}
