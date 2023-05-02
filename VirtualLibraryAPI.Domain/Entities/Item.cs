using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.DTOs;
using static System.Reflection.Metadata.BlobBuilder;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of items
    /// </summary>
    public class Item
    {
        /// <summary>
        /// ID of item
        /// </summary>
        [Key, ForeignKey("ItemID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemID { get; set; }
        /// <summary>
        /// Name of item
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public virtual Type Type { get; set; } 
        /// <summary>
        /// Publishing date of item
        /// </summary>
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Publisher of item
        /// </summary>
        public string Publisher { get; set; } = string.Empty;
    }
}

