using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibraryAPI.Common;
using VirtualLibraryAPI.Domain.Entities;
using UserType = VirtualLibraryAPI.Domain.Entities.UserType;

namespace VirtualLibraryAPI.Domain.EntitiesConfiguration
{
    /// <summary>
    /// Configuration for UserType
    /// </summary>
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        /// <summary>
        /// Builder configuration for UserType
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(e => e.UserTypeId);
            builder.Property(e => e.UserTypeId)
                .HasConversion<short>()
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(e => e.UserTypeName)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false);

            builder.HasData(Enum.GetValues(typeof(UserTypes))
                .Cast<UserTypes>()
                .Select(e => new UserType
                {
                    UserTypeId = e,
                    UserTypeName = e.ToString()
                }));
        }
    }
}
