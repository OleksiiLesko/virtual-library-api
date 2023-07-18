using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemType",
                columns: table => new
                {
                    ItemTypeId = table.Column<short>(type: "smallint", nullable: false),
                    ItemTypeName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.ItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<short>(type: "smallint", maxLength: 25, nullable: false),
                    PublishingDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Item_ItemType",
                        column: x => x.Type,
                        principalTable: "ItemType",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MagazinesIssueNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MagazineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Article_Item",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Book_Item",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                });

            migrationBuilder.CreateTable(
                name: "Copies",
                columns: table => new
                {
                    CopyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    UserID = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copies", x => x.CopyID);
                    table.ForeignKey(
                        name: "FK_Copy_Item",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Copy_User",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Magazines",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    IssueNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magazines", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Magazine_Item",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                });

            migrationBuilder.InsertData(
                table: "ItemType",
                columns: new[] { "ItemTypeId", "ItemTypeName" },
                values: new object[,]
                {
                    { (short)1, "Book" },
                    { (short)2, "Article" },
                    { (short)3, "Magazine" },
                    { (short)4, "Copy" }
                });

            migrationBuilder.InsertData(
             table: "Users",
             columns: new[] { "UserID", "FirstName", "LastName", "UserTypes" },
             values: new object[] { 10, "John", "Doe", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Copies_ItemID",
                table: "Copies",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserID",
                table: "Copies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Type",
                table: "Items",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Copies");

            migrationBuilder.DropTable(
                name: "Magazines");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemType");
        }
    }
}
