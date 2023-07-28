using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of clients
    /// </summary>
    public class Client
    {
        /// <summary>
        ///  ID of client
        /// </summary>
        public int ClientID { get; set; }
        /// <summary>
        /// First name of user
        /// </summary> 
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// Last name of user
        /// </summary>
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<Copy> Copies { get; set; }
    }
}
