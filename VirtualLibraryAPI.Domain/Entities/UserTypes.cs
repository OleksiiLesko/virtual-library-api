using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Enumeration of user types
    /// </summary>
    public enum  UserTypes
    {
        Administrator = 0,
        Manager = 1
    }
}
