namespace VirtualLibraryAPI.Domain.Models
{
    /// <summary>
    /// Table of items
    /// </summary>
    public class Items
    {
        /// <summary>
        /// ID of item
        /// </summary>
        public int ItemID { get; set; }
        /// <summary>
        /// Name of item
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Type of item (book, magazine, article, copy)
        /// </summary>
        public ItemType Type { get; set; }
        /// <summary>
        /// Publishing date of item
        /// </summary>
        public DateTime PublishingDate { get; set; }
        /// <summary>
        /// Publisher of item
        /// </summary>
        public string? Publisher { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Articles>? Articles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Books>? Books { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Magazines>? Magazines { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Copies>? Copies { get; set; }
    }
}
