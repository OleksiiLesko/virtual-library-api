using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection;
using System.IO;
using VirtualLibraryAPI.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace VirtualLibraryAPI.Domain
{
    /// <summary>
    /// Application context with database
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        ///  Items entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Item> Items { get; set; }
        /// <summary>
        ///  Books entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Book> Books { get; set; }
        /// <summary>
        ///  Articles entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Article> Articles { get; set; }
        /// <summary>
        ///  Magazines entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Magazine> Magazines { get; set; }
        /// <summary>
        ///  Copies entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Copy> Copies { get; set; }
        /// <summary>
        ///  ItemType entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<ItemType> ItemTypes { get; set; }

        /// <summary>
        /// Application context that has options 
        /// </summary>
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        /// <summary>
        /// Create models
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
            modelBuilder.Entity<Item>()
               .ToTable("Items");

            modelBuilder.Entity<Article>()
                 .ToTable("Articles");

            modelBuilder.Entity<Book>()
                .ToTable("Books");

            modelBuilder.Entity<Magazine>()
                .ToTable("Magazines");

            modelBuilder.Entity<Copy>()
                .ToTable("Copies");
        }
    }
}
