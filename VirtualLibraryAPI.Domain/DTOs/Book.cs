
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
    /// Book DTO
    /// </summary>
   
    public class Book
    {
        /// <summary>
        ///  ID of book
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? BookID { get; set; }
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
        /// Name of book
        /// </summary>
        [Required]
        public string Name { get; set; } 
        /// <summary>
        /// Publishing date of book
        /// </summary>
        [Required]
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Publisher of book
        /// </summary>
        [Required]
        public string Publisher { get; set; } 
        /// <summary>
        /// Author of book
        /// </summary>
        [Required]
        public string Author { get; set; } 
        /// <summary>
        /// ISBN of book
        /// </summary> 
        [Required]
        public string ISBN { get; set; } 
        /// <summary>
        /// Copy information
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CopyInfo? CopyInfo { get; set; }
    }
}
