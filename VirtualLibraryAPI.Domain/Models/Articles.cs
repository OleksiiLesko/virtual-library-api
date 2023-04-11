namespace VirtualLibraryAPI.Domain.Models
{
    /// <summary>
    /// Table of articles
    /// </summary>
    public class Articles : Items
    {
        /// <summary>
        /// 
        /// </summary>
        public Items? Item { get; set; }
        /// <summary>
        /// Author of article 
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// Version of article 
        /// </summary>
        public float Version { get; set; }
        /// <summary>
        /// Magazines issue number of article 
        /// </summary>
        public string? MagazinesIssueNumber { get; set; }
        /// <summary>
        /// Magazine name of article 
        /// </summary>
        public string? MagazineName { get; set; }
    }
}
