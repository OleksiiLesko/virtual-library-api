using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        /// First name of user
        /// </summary>
        public string FirstName { get; set; } 
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; } 
    }
}
