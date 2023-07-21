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
    /// Configuration for DepartmentType
    /// </summary>
    public class DepartmentTypeConfiguration : IEntityTypeConfiguration<DepartmentType>
    {
        /// <summary>
        /// Builder configuration for DepartmentType
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<DepartmentType> builder)
        {
            builder.HasKey(e => e.TypeId);
            builder.Property(e => e.TypeId)
                .HasConversion<short>()
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.HasData(Enum.GetValues(typeof(DepartmentTypes))
                .Cast<DepartmentTypes>()
                .Select(e => new DepartmentType
                {
                    TypeId = e,
                    TypeName = e.ToString()
                }));
        }
    }
}
