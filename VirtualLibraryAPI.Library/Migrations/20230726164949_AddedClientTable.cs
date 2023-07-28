using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Copies",
                table: "Copies");

            migrationBuilder.AddColumn<int>(
                name: "ClientID",
                table: "Copies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Copies_ClientID",
                table: "Copies",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Copies",
                table: "Copies",
                column: "ClientID",
                principalTable: "Clients",
                principalColumn: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Users_UserID",
                table: "Copies",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Copies",
                table: "Copies");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Users_UserID",
                table: "Copies");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Copies_ClientID",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "Copies");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Copies",
                table: "Copies",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }
    }
}
