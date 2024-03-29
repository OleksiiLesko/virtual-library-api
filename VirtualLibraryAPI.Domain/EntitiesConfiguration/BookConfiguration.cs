﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.Entities;

namespace VirtualLibraryAPI.Domain.EntitiesConfiguration
{
    /// <summary>
    /// Configuration of book
    /// </summary>
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        /// <summary>
        /// Builder configuration for book
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.ItemID);

            builder.Property(e => e.ItemID)
              .ValueGeneratedNever();

            builder.Property(e => e.Author)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ISBN)
                .HasMaxLength(50)
                .IsRequired();

            builder.Ignore(e => e.Type);
            builder.Ignore(e => e.CopyInfo);

            builder.HasOne(e => e.Item)
             .WithOne(e => e.Book)
             .HasForeignKey<Book>(e => e.ItemID)
             .OnDelete(DeleteBehavior.NoAction)
             .HasConstraintName("FK_Book_Item");
        }
    }
}
