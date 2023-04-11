namespace VirtualLibraryAPI.Domain.Models
{
    /// <summary>
    /// Table of Books
    /// </summary>
    public class Books : Items
    {
        /// <summary>
        /// 
        /// </summary>
        public Items? Item { get; set; }
        /// <summary>
        /// Author of book
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// ISBN of book
        /// </summary>
        public string? ISBN { get; set; }
    }
}
