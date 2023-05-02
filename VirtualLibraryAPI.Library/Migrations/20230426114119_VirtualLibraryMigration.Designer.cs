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
    [Migration("20230426114119_VirtualLibraryMigration")]
    partial class VirtualLibraryMigration
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MagazineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MagazinesIssueNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemID");

                    b.ToTable("Articles", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Book", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemID");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.Copy", b =>
                {
                    b.Property<int?>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ItemID"));

                    b.Property<int?>("CopyID")
                        .HasColumnType("int");

                    b.HasKey("ItemID");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishingDate")
                        .HasColumnType("datetime2");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("ItemID");

                    b.ToTable("Items", (string)null);
                });

            modelBuilder.Entity("VirtualLibraryAPI.Domain.Entities.ItemType", b =>
                {
                    b.Property<short>("ItemTypeId")
                        .HasColumnType("smallint")
                        .HasColumnName("ItemTypeId");

                    b.Property<string>("ItemTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ItemTypeName");

                    b.HasKey("ItemTypeId");

                    b.ToTable("ItemTypes");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<string>("IssueNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemID");

                    b.ToTable("Magazines", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
