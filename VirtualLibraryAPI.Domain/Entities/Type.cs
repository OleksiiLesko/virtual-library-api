using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Enum for types
    /// </summary>
    public enum Type : short
    {
        Book = 1,
        Article = 2,
        Magazine = 3,
        Copy = 4
    }
}
