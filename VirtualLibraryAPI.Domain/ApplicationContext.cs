using Microsoft.EntityFrameworkCore;
namespace VirtualLibraryAPI.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DbSet<MyTable> Tables { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();   
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MyTable>();
        }
    }
}
