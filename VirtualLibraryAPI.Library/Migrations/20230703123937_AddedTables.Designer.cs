﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VirtualLibraryAPI.Domain;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230703123937_AddedTables")]
    partial class AddedTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Article", b =>
                {
                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MagazineName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MagazinesIssueNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("ItemID");

                    b.ToTable("Articles", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Book", b =>
                {
                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ItemID");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Copy", b =>
                {
                    b.Property<int>("CopyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CopyID"));

                    b.Property<DateTime>("ExpirationDate")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("ItemID")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("CopyID");

                    b.HasIndex("ItemID");

                    b.HasIndex("UserID");

                    b.ToTable("Copies", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("PublishingDate")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<short>("Type")
                        .HasMaxLength(25)
                        .HasColumnType("smallint");

                    b.HasKey("ItemID");

                    b.HasIndex("Type");

                    b.ToTable("Items", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.ItemType", b =>
                {
                    b.Property<short>("ItemTypeId")
                        .HasColumnType("smallint");

                    b.Property<string>("ItemTypeName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .IsUnicode(false)
                        .HasColumnType("varchar(60)");

                    b.HasKey("ItemTypeId");

                    b.ToTable("ItemType");

                    b.HasData(
                        new
                        {
                            ItemTypeId = (short)1,
                            ItemTypeName = "Book"
                        },
                        new
                        {
                            ItemTypeId = (short)2,
                            ItemTypeName = "Article"
                        },
                        new
                        {
                            ItemTypeId = (short)3,
                            ItemTypeName = "Magazine"
                        },
                        new
                        {
                            ItemTypeId = (short)4,
                            ItemTypeName = "Copy"
                        });
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Magazine", b =>
                {
                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.Property<string>("IssueNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ItemID");

                    b.ToTable("Magazines", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserID");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Article", b =>
                {
                    b.HasOne("VirtualLibraryAPI.Domain.Entities.Item", "Item")
                        .WithOne("Article")
                        .HasForeignKey("VirtualLibraryAPI.Domain.Entities.Article", "ItemID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Article_Item");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Book", b =>
                {
                    b.HasOne("VirtualLibraryAPI.Domain.Entities.Item", "Item")
                        .WithOne("Book")
                        .HasForeignKey("VirtualLibraryAPI.Domain.Entities.Book", "ItemID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Book_Item");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Copy", b =>
                {
                    b.HasOne("VirtualLibraryAPI.Domain.Entities.Item", "Item")
                        .WithOne("Copy")
                        .HasForeignKey("VirtualLibraryAPI.Domain.Entities.Copy", "ItemID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Copy_Item");

                    b.HasOne("VirtualLibraryAPI.Domain.Entities.User", "User")
                        .WithMany("Copies")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Copy_User");

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Item", b =>
                {
                    b.HasOne("VirtualLibraryAPI.Domain.Entities.ItemType", "ItemType")
                        .WithMany("Item")
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Item_ItemType");

                    b.Navigation("ItemType");
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Magazine", b =>
                {
                    b.HasOne("VirtualLibraryAPI.Domain.Entities.Item", "Item")
                        .WithOne("Magazine")
                        .HasForeignKey("VirtualLibraryAPI.Domain.Entities.Magazine", "ItemID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_Magazine_Item");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Item", b =>
                {
                    b.Navigation("Article")
                        .IsRequired();

                    b.Navigation("Book")
                        .IsRequired();

                    b.Navigation("Copy")
                        .IsRequired();

                    b.Navigation("Magazine")
                        .IsRequired();
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.ItemType", b =>
                {
                    b.Navigation("Item");
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.User", b =>
                {
                    b.Navigation("Copies");
                });
#pragma warning restore 612, 618
        }
    }
}
