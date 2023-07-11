﻿using System.Text.Json.Serialization;
using Newtonsoft.Json;
using VirtualLibraryAPI.Common;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using Type = VirtualLibraryAPI.Common.Type;

namespace VirtualLibraryAPI.Domain.DTOs
{
    /// <summary>
    /// Copy DTO
    /// </summary>
    public class Copy
    {
        /// <summary>
        ///  ID of copy
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        ///  ID of user
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? UserID { get; set; }
        /// <summary>
        /// ID of copy
        public int CopyID { get; set; }
        /// <summary>
        /// Name of copy
        /// </summary>
        public string? Name { get; set; } 
        /// <summary>
        /// Publishing date of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? PublishingDate { get; set; }
        /// <summary>
        /// Publisher of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Publisher { get; set; } 
        /// <summary>
        /// Author of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Author { get; set; } 
        /// <summary>
        /// ISBN of copy
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull )]

        public string? ISBN { get; set; } 
        /// <summary>
        /// Status of copy availability
        /// </summary>
        [JsonIgnore]
        public bool IsAvailable { get; set; }
        /// <summary>
        /// Expiration date of copy
        /// </summary> 
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// Type of item
        /// </summary>
        //[JsonIgnore]
        public Type Type { get; set; }
        /// <summary>
        /// Types of item
        /// </summary>
        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        //public string? Types { get; set; } = string.Empty;

    }
}
