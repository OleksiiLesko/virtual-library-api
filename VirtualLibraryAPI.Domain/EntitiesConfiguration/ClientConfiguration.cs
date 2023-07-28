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
    /// Configuration of client
    /// </summary>
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        /// <summary>
        /// Builder configuration for user
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(e => e.ClientID);

            builder.Property(e => e.ClientID)
               .IsRequired()
             .UseIdentityColumn();

            builder.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.HasMany(e => e.Copies)
              .WithOne(e => e.Client)
              .HasForeignKey(e => e.ClientID)
              .OnDelete(DeleteBehavior.NoAction)
              .HasConstraintName("FK_Client_Copies");
        }
    }
}
