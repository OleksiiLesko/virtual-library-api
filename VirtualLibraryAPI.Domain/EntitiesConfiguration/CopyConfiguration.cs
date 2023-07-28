using Microsoft.EntityFrameworkCore;
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
    /// Configuration of copy
    /// </summary>
    public class CopyConfiguration : IEntityTypeConfiguration<Copy>
    {
        /// <summary>
        /// Builder configuration for copy
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Copy> builder)
        {
            builder.HasKey(e => e.CopyID);

            builder.Property(e => e.CopyID)
              .ValueGeneratedOnAdd();

            builder.Property(e => e.ItemID)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.ClientID)
            .ValueGeneratedNever()
            .IsRequired(false);

            builder.Property(e => e.IsAvailable)
              .IsRequired();

            builder.Property(e => e.ExpirationDate)
                  .HasMaxLength(50)
                  .IsRequired(false);

            builder.Ignore(e => e.BookingPeriod);

            builder.Ignore(e => e.Type);

            builder.HasOne(e => e.Item)
                .WithOne(e => e.Copy)
                .HasForeignKey<Copy>(e => e.ItemID)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Copy_Item");

            builder.HasIndex(e => e.ItemID).IsUnique(false);

            builder.HasIndex(e => e.ClientID).IsUnique(false);

        }
    }
}
