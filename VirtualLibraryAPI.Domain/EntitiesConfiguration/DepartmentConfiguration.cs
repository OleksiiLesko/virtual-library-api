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
    /// Configuration of department
    /// </summary>
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        /// <summary>
        /// Builder configuration for department
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.DepartmentID);

            builder.Property(e => e.DepartmentID)
                .IsRequired()
              .ValueGeneratedOnAdd();

            builder.Property(e => e.DepartmentName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.HasOne(d => d.User)
               .WithOne(u => u.Department)
               .HasForeignKey<Department>(u => u.DepartmentID)
               .OnDelete(DeleteBehavior.NoAction)
               .HasConstraintName("FK_Department_User");

            builder.HasMany(i => i.Item)
             .WithOne(d => d.Department)
              .HasForeignKey(e => e.DepartmentID)
               .OnDelete(DeleteBehavior.NoAction)
              .HasConstraintName("FK_Department_Items");
        }
    }
}
