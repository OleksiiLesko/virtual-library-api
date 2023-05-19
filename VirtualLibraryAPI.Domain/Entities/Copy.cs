﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VirtualLibraryAPI.Domain.Entities
{
    /// <summary>
    /// Table of Copies
    /// </summary>
    public class Copy 
    {
        /// <summary>
        ///  ID of item
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// Connect with Item
        /// </summary>
        public virtual Item Item { get; set; }
        /// <summary>
        /// ID of copy
        /// </summary>
        public int CopyID { get; set; }
        /// <summary>
        /// Type of  copy
        /// </summary>
        public virtual Type Type { get; set; } = Type.Copy;
    }
}