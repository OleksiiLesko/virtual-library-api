using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using VirtualLibraryAPI.Domain.Models;

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
        public DbSet<Items> Items { get; set; }
        /// <summary>
        ///  Books entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Books> Books { get; set; }
        /// <summary>
        ///  Articles entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Articles> Articles { get; set; }
        /// <summary>
        ///  Magazines entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Magazines> Magazines { get; set; }
        /// <summary>
        ///  Copies entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Copies> Copies { get; set; }

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
            modelBuilder.Entity<Items>()
                .ToTable("Items")
                .HasKey(i => i.ItemID);

            modelBuilder.Entity<Articles>()
                 .ToTable("Articles")
                .HasOne(a => a.Item)
                .WithMany(i => i.Articles)
                .HasForeignKey(a => a.ItemID);

            modelBuilder.Entity<Books>()
                .ToTable("Books")
                .HasOne(b => b.Item)
                .WithMany(i => i.Books)
                .HasForeignKey(b => b.ItemID);

            modelBuilder.Entity<Magazines>()
                .ToTable("Magazines")
                .HasOne(m => m.Item)
                .WithMany(i => i.Magazines)
                .HasForeignKey(m => m.ItemID);

            modelBuilder.Entity<Copies>()
                .ToTable("Copies")
                .HasOne(c => c.Item)
                .WithMany(i => i.Copies)
                .HasForeignKey(c => c.ItemID);
        }
    }
}
