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
    public class MagazineConfiguration : IEntityTypeConfiguration<Magazine>
    {
        public void Configure(EntityTypeBuilder<Magazine> builder)
        {
            builder.HasKey(e => e.ItemID);

            builder.Property(e => e.ItemID)
              .ValueGeneratedNever(); 

            builder.Property(e => e.IssueNumber)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Ignore(e => e.Type);
            builder.Ignore(e => e.CopyInfo);

            builder.HasOne(e => e.Item)
           .WithOne(e => e.Magazine)
           .HasForeignKey<Magazine>(e => e.ItemID)
           .OnDelete(DeleteBehavior.NoAction)
           .HasConstraintName("FK_Magazine_Item");
        }
    }
}
