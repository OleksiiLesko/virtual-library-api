 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Domain.DTOs
{
    /// <summary>
    /// Magazine DTO
    /// </summary>
    public class Magazine
    {
        ///  ID of magazine
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? MagazineID { get; set; }
        /// <summary>
        /// ID of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CopyID { get; set; }
        /// <summary>
        ///  ID of department
        /// </summary>
        [Required]
        public int DepartmentID { get; set; }
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
        /// <summary>
        /// Copy information
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CopyInfo? CopyInfo { get; set; }
    }
}
