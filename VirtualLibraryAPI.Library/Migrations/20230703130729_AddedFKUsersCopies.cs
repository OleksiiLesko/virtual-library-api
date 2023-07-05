using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedFKUsersCopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Copy_User",
            //    table: "Copies");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_UserID",
            //    table: "Users",
            //    column: "UserID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_User_Copies",
            //    table: "Copies",
            //    column: "UserID",
            //    principalTable: "Users",
            //    principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_User_Copies",
            //    table: "Copies");

            //migrationBuilder.DropIndex(
            //    name: "IX_Users_UserID",
            //    table: "Users");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Copy_User",
            //    table: "Copies",
            //    column: "UserID",
            //    principalTable: "Users",
            //    principalColumn: "UserID");
        }
    }
}
