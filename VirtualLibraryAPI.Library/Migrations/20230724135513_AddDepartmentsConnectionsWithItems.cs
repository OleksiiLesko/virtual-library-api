using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentsConnectionsWithItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Items_Department",
            //    table: "Items");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Department_Items",
            //    table: "Items",
            //    column: "DepartmentID",
            //    principalTable: "Departments",
            //    principalColumn: "DepartmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Items",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Department",
                table: "Items",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");
        }
    }
}
