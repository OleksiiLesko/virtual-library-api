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
    /// Configuration of user
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Builder configuration for user
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserID);

            builder.Property(e => e.UserID)
               .IsRequired()
             .UseIdentityColumn();

            builder.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(e => e.UserTypes)
              .HasMaxLength(25)
              .IsRequired();

            builder.HasMany(e => e.Copies)
              .WithOne(e => e.User)
              .HasForeignKey(e => e.UserID)
              .OnDelete(DeleteBehavior.NoAction)
              .HasConstraintName("FK_User_Copies");

            builder.HasOne(e => e.UserType)
             .WithMany(e => e.User)
             .HasForeignKey(e => e.UserTypes)
             .HasConstraintName("FK_User_UserType");


        }
    }
}
