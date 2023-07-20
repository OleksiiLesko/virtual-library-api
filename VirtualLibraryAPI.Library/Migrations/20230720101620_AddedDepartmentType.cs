using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedDepartmentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "DepartmentTypes",
                table: "Items",
                type: "smallint",
                maxLength: 25,
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "DepartmentType",
                columns: table => new
                {
                    TypeId = table.Column<short>(type: "smallint", nullable: false),
                    TypeName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentType", x => x.TypeId);
                });

            migrationBuilder.InsertData(
                table: "DepartmentType",
                columns: new[] { "TypeId", "TypeName" },
                values: new object[,]
                {
                    { (short)0, "Fantasy" },
                    { (short)1, "Adventure" },
                    { (short)2, "Science" },
                    { (short)3, "Romance" },
                    { (short)4, "Horror" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_DepartmentTypes",
                table: "Items",
                column: "DepartmentTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_DepartmentType",
                table: "Items",
                column: "DepartmentTypes",
                principalTable: "DepartmentType",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_DepartmentType",
                table: "Items");

            migrationBuilder.DropTable(
                name: "DepartmentType");

            migrationBuilder.DropIndex(
                name: "IX_Items_DepartmentTypes",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DepartmentTypes",
                table: "Items");
        }
    }
}
