using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConnectItemWithDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Department_Items",
            //    table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_DepartmentID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_DepartmentID",
                table: "Items",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Items",
                table: "Items",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");
        }
    }
}
