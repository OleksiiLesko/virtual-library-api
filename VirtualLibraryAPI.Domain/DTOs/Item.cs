using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using Type = VirtualLibraryAPI.Common.Type;

namespace VirtualLibraryAPI.Domain.DTOs
{
    /// <summary>
    /// Item DTO
    /// </summary>
    public class Item
    {
        /// <summary>
        /// ID of item
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// CopyID of item
        /// </summary>
        public int CopyID { get; set; }
        /// <summary>
        /// Name of item
        /// </summary>
        public string Name { get; set; } 
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Publishing date of item
        /// </summary>
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Publisher of item
        /// </summary>
        public string Publisher { get; set; } 
    }
}
