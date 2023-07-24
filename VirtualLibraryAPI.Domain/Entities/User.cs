using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of users
    /// </summary>
    public class User
    {
        /// <summary>
        ///  ID of user
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// First name of user
        /// </summary> 
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        /// <summary>
        /// Type of user 
        /// </summary>
        public UserTypes UserTypes { get; set; }
        /// <summary>
        ///  ID of department
        /// </summary>
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Copy> Copies { get; set;}
        public virtual UserType UserType { get; set; }
    }
}
