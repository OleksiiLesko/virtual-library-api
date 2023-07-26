using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of departments
    /// </summary>
    public class Department
    {
        /// <summary>
        ///  ID of department
        /// </summary>
        public int DepartmentID { get; set; }
        /// <summary>
        /// Name of department
        /// </summary> 
        public string DepartmentName { get; set; } = string.Empty;
        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Item> Items { get; set; } 
    }
}
