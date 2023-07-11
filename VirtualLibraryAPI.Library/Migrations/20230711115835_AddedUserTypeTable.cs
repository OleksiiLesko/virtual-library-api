using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "UserTypeId",
                table: "Users",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    UserTypeId = table.Column<short>(type: "smallint", nullable: false),
                    UserTypeName = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.UserTypeId);
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "UserTypeId", "UserTypeName" },
                values: new object[,]
                {
                    { (short)0, "Administrator" },
                    { (short)1, "Client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserType_UserTypeId",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserType",
                principalColumn: "UserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserType_UserTypeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Users");
        }
    }
}
