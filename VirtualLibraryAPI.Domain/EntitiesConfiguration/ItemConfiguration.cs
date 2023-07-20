using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    /// Configuration of item
    /// </summary>
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        /// <summary>
        /// Builder configuration for Item
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(e => e.ItemID);

            builder.Property(e => e.ItemID)
                .IsRequired()
              .UseIdentityColumn();

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.PublishingDate)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Publisher)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Type)
               .HasMaxLength(25)
               .IsRequired();

            builder.Property(e => e.DepartmentTypes)
               .HasMaxLength(25)
               .IsRequired();

            builder.HasOne(e => e.ItemType)
               .WithMany(e => e.Item)
               .HasForeignKey(e => e.Type)
               .HasConstraintName("FK_Item_ItemType");

            builder.HasOne(e => e.DepartmentType)
            .WithMany(e => e.Item)
            .HasForeignKey(e => e.DepartmentTypes)
            .HasConstraintName("FK_Item_DepartmentType");
        }
    }
}
