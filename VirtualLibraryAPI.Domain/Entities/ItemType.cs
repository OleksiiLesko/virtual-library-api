﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibraryAPI.Domain.Entities
{

    // <summary>
    /// Description for enum type
    /// </summary>
    public class ItemType
    {
        /// <summary>
        /// Type id
        /// </summary>
        public Type ItemTypeId { get; set; } 
        /// <summary>
        /// Type name
        /// </summary>
        public string ItemTypeName { get; set; } = string.Empty;
        /// <summary>
        /// Connect with Item
        /// </summary>
        public virtual ICollection<Item> Item { get; set; }
    }
}