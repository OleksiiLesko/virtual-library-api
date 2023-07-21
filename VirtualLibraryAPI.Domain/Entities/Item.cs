
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
        public int ItemID { get; set; }
        /// <summary>
        /// Name of item
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// Publishing date of item
        /// </summary>
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Publisher of item
        /// </summary>
        public string Publisher { get; set; } = string.Empty;
        public virtual Book Book { get; set; }
        public virtual Article Article { get; set; }
        public virtual Magazine Magazine { get; set; }
        public virtual Copy Copy { get; set; }
        public virtual ItemType ItemType { get; set; }

    }
}

