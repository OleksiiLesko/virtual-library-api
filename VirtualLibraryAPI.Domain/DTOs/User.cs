using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace VirtualLibraryAPI.Domain.DTOs
{
    /// <summary>
    /// User DTO
    /// </summary>
    public class User
    {
        /// <summary>
        ///  ID of user
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? UserID { get; set; }
        /// <summary>
        /// ID of department
        /// </summary>
        [Required]
        public int DepartmentID { get; set; }
        /// <summary>
        /// First name of user
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of user
        /// </summary>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Type of user 
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public UserType UserType { get; set; }
    }
}
