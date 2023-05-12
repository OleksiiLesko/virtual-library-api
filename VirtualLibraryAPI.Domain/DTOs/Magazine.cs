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
    public class Magazine
    {
        ///  ID of magazine
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
        /// Name of magazine
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Publishing date of magazine
        /// </summary>
        [Required]
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Publisher of magazine
        /// </summary>
        [Required]
        public string Publisher { get; set; }
        /// <summary>
        /// Issue number of magazine
        /// </summary>
        [Required]
        public string IssueNumber { get; set; }
    }
}
