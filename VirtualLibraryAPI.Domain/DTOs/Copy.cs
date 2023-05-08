using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.DTOs
{
    public class Copy
    {
        /// <summary>
        ///  ID of item
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// ID of copy
        /// </summary>
        public int CopyID { get; set; }
        /// <summary>
        /// Name of item
        /// </summary>
        public string? Name { get; set; } = string.Empty;
        /// <summary>
        /// Publishing date of item
        /// </summary>
        public DateTime? PublishingDate { get; set; }
        /// <summary>
        /// Publisher of item
        /// </summary>
        public string? Publisher { get; set; } = string.Empty;
        /// <summary>
        /// Author of item
        /// </summary>
        public string? Author { get; set; } = string.Empty;
        /// <summary>
        /// ISBN of item
        /// </summary>
        public string? ISBN { get; set; } = string.Empty;
    }
}
