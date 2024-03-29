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
    /// Configuration of article
    /// </summary>
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        /// <summary>
        /// Builder configuration for article
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(e => e.ItemID);

            builder.Property(e => e.ItemID)
              .ValueGeneratedNever();

            builder.Property(e => e.MagazineName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(e => e.MagazinesIssueNumber)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(e => e.Version)
                    .HasMaxLength(25)
                    .IsRequired();

            builder.Property(e => e.Author)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Ignore(e => e.Type);
            builder.Ignore(e => e.CopyInfo);

            builder.HasOne(e => e.Item)
         .WithOne(e => e.Article)
         .HasForeignKey<Article>(e => e.ItemID)
         .OnDelete(DeleteBehavior.NoAction)
         .HasConstraintName("FK_Article_Item");
        }
    }
}
