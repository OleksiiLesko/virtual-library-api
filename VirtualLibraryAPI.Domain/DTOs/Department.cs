using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.DTOs
{
    public class Department
    {
        /// <summary>
        ///  ID of department
        /// </summary>
        [Required]
        public int DepartmentID { get; set; }
        /// <summary>
        /// Name of department
        /// </summary> 
        [Required]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
