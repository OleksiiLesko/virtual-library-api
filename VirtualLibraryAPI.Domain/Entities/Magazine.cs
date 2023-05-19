using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of magazines
    /// </summary>
    public class Magazine 
    {
        /// <summary>
        /// ID of magazine
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// Connect with Item
        /// </summary>
        public virtual Item Item { get; set; }
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public Type Type { get; set; } = Type.Magazine;
        /// <summary>
        /// Issue number of magazine
        /// </summary>
        public string IssueNumber { get; set; } = string.Empty;
    }
}
