﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Domain.Entities;
using Type = VirtualLibraryAPI.Domain.Entities.Type;

namespace VirtualLibraryAPI.Domain.EntitiesConfiguration
{
    /// <summary>
    /// Configuration for ItemType
    /// </summary>
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        /// <summary>
        /// Builder configuration for ItemType
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder.HasKey(e => e.ItemTypeId);
            builder.Property(e => e.ItemTypeId)
                .HasConversion<short>()
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(e => e.ItemTypeName)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.HasData(Enum.GetValues(typeof(Type))
                .Cast<Type>()
                .Select(e => new ItemType
                {
                    ItemTypeId = e,
                    ItemTypeName = e.ToString()
                }));
        }
    }
}
