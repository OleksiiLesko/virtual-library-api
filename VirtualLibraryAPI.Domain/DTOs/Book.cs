
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.DTOs
{
    /// <summary>
    /// Book DTO
    /// </summary>
   
    public class Book
    {
        /// <summary>
        /// Item ID of book
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BookID { get; set; }
        /// <summary>
        /// ID of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CopyID { get; set; }
        /// <summary>
        /// Count of copies
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CountOfCopies { get; set; }
        /// <summary>
        /// Name of item
        /// </summary>
        [Required]
        public string? Name { get; set; } = null;
        /// <summary>
        /// Publishing date of item
        /// </summary>
        [Required]
        public DateTime? PublishingDate { get; set; }
        /// <summary>
        /// Publisher of item
        /// </summary>
        [Required]
        public string? Publisher { get; set; } = null;
        /// <summary>
        /// Author of item
        /// </summary>
        [Required]
        public string? Author { get; set; } = null;
        /// <summary>
        /// ISBN of item
        /// </summary> 
        [Required]
        public string? ISBN { get; set; } = null;
    }
}
