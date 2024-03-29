﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection;
using System.IO;
using VirtualLibraryAPI.Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;
using VirtualLibraryAPI.Domain.EntitiesConfiguration;
using System.Reflection.Metadata;

namespace VirtualLibraryAPI.Domain
{
    /// <summary>
    /// Application context with database
    /// </summary>
    public class ApplicationContext : DbContext
    {
        /// <summary>
        ///  Users entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }
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
        public DbSet<ItemType> ItemType { get; set; }
        /// <summary>
        ///  UserType entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<UserType> UserType { get; set; }
        /// <summary>
        ///  Department entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Department> Departments { get; set; }
        /// <summary>
        ///  Client entity type  used to interact with a corresponding table in the database.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

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
            modelBuilder.Entity<Client>()
              .ToTable("Clients");

            modelBuilder.Entity<Department>()
              .ToTable("Departments");

            modelBuilder.Entity<User>()
               .ToTable("Users");

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

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new MagazineConfiguration());
            modelBuilder.ApplyConfiguration(new CopyConfiguration());
        }
    }
}
