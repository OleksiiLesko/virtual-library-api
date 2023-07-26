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
             .UseIdentityColumn();

            builder.Property(e => e.DepartmentName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.HasMany(e => e.Items)
           .WithOne(e => e.Department)
           .HasForeignKey(e => e.DepartmentID)
           .OnDelete(DeleteBehavior.NoAction)
           .HasConstraintName("FK_Department_Items");

            builder.HasMany(e => e.Users)
           .WithOne(e => e.Department)
           .HasForeignKey(e => e.DepartmentID)
           .OnDelete(DeleteBehavior.NoAction)
           .HasConstraintName("FK_Department_Users");

        }
    }
}
