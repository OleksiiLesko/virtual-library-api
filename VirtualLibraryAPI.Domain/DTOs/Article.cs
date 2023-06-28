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
    /// Magazine DTO
    /// </summary>
    public class Article
    {
        /// <summary>
        ///  ID of article
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ArticleID { get; set; }
        /// <summary>
        /// ID of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? CopyID { get; set; }
        /// <summary>
        /// Name of article
        /// </summary>
        [Required]
        public string Name { get; set; } 
        /// <summary>
        /// Publishing date of article
        /// </summary>
        [Required]
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Author of article 
        /// </summary>
        [Required]
        public string Author { get; set; } 
        /// <summary>
        /// Publisher of article
        /// </summary>
        [Required]
        public string Publisher { get; set; } 
        /// <summary>
        /// Author of article
        /// </summary>
        [Required]
        public string Version { get; set; }
        /// <summary>
        /// Magazines issue number of article 
        /// </summary>
        [Required]
        public string MagazinesIssueNumber { get; set; }                        
        /// <summary>
        /// Magazine name of article 
        /// </summary>
        [Required]
        public string MagazineName { get; set; } 
        /// <summary>
        /// Copy information
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CopyInfo? CopyInfo { get; set; }
    }
}
