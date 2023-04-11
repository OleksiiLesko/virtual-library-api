namespace VirtualLibraryAPI.Domain.Models
{
    /// <summary>
    /// Table of magazines
    /// </summary>
    public class Magazines : Items
    {
        /// <summary>
        /// 
        /// </summary>
        public Items? Item { get; set; }
        /// <summary>
        /// Issue number of magazine
        /// </summary>
        public int IssueNumber { get; set; }
    }
}
