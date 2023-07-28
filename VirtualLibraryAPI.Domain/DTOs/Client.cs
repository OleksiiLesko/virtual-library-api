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
    /// Client DTO
    /// </summary>
    public class Client
    {
        /// <summary>
        ///  ID of user
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ClientID { get; set; }
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
    }
}
