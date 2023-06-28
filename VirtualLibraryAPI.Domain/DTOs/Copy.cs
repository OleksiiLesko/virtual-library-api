using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace VirtualLibraryAPI.Domain.DTOs
{
    public class Copy
    {
        /// <summary>
        ///  ID of copy
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// ID of copy
        public int CopyID { get; set; }
        /// <summary>
        /// Name of copy
        /// </summary>
        public string? Name { get; set; } = string.Empty;
        /// <summary>
        /// Publishing date of copy
        /// </summary>
        public DateTime? PublishingDate { get; set; }
        /// <summary>
        /// Publisher of copy
        /// </summary>
        public string? Publisher { get; set; } = string.Empty;
        /// <summary>
        /// Author of copy
        /// </summary>
        public string? Author { get; set; } = string.Empty;
        /// <summary>
        /// ISBN of copy
        /// </summary>
        public string? ISBN { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public bool IsAvailable { get; set; }
        /// <summary>
        /// Expiration date of copy
        /// </summary> 
        public DateTime ExpirationDate { get; set; }
        public Type Type { get; set; }
    }
}
