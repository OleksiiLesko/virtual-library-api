using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedDepartmentConnectiomWithUserAndItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_User_Department",
            //    table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Users",
                table: "Users",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Users",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Department",
                table: "Users",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
