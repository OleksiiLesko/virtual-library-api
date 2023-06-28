using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.DTOs;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of Books
    /// </summary>
    public class Book
    {
        /// <summary>
        ///  ID of book
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// Connect with Item
        /// </summary>
        public virtual Item Item { get; set; }
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public virtual Type Type { get; set; } = Type.Book;
        /// <summary>
        /// Author of book
        /// </summary>
        public string Author { get; set; } = string.Empty;
        /// <summary>
        /// ISBN of book
        /// </summary>
        public string ISBN { get; set; } = string.Empty;
        /// <summary>
        /// Information about copy of book
        /// </summary>
        public CopyInfo? CopyInfo { get; set; }
    }
}



