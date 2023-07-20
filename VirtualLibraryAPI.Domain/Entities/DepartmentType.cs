using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Description for enum DepartmentType
    /// </summary>
    public class DepartmentType
    {
        /// <summary>
        /// Type id
        /// </summary>
        public DepartmentTypes TypeId { get; set; }
        /// <summary>
        /// Type name
        /// </summary>
        public string TypeName { get; set; } = string.Empty;
        /// <summary>
        /// Connect with User
        /// </summary>
        public virtual ICollection<Item> Item { get; set; }
    }
}
