using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Description for enum UserType
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// Type id
        /// </summary>
        public UserTypes UserTypeId { get; set; }
        /// <summary>
        /// Type name
        /// </summary>
        public string UserTypeName { get; set; } = string.Empty;
        /// <summary>
        /// Connect with User
        /// </summary>
        public virtual ICollection<User> User { get; set; }
    }
}
