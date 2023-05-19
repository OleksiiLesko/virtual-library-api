using System;
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
    /// Table of articles
    /// </summary>
    public class Article 
    {
        /// <summary>
        ///  ID of article
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// Connect with Item
        /// </summary>
        public virtual Item Item { get; set; }
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public virtual Type Type { get; set; } = Type.Article;
        /// <summary>
        /// Author of article 
        /// </summary>
        public string Author { get; set; } = string.Empty;
        /// <summary>
        /// Version of article 
        /// </summary>
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// Magazines issue number of article 
        /// </summary>
        public string MagazinesIssueNumber { get; set; } = string.Empty;
        /// <summary>
        /// Magazine name of article 
        /// </summary>
        public string MagazineName { get; set; } = string.Empty;
    }
}
