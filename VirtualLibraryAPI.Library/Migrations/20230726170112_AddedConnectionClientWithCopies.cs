using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedConnectionClientWithCopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Users_UserID",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_UserID",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Copies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Copies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_UserID",
                table: "Copies",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Users_UserID",
                table: "Copies",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
