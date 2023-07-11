using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedConnectionUserWithUserType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserType_UserTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Users");

            migrationBuilder.AddColumn<short>(
                name: "UserTypes",
                table: "Users",
                type: "smallint",
                maxLength: 25,
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypes",
                table: "Users",
                column: "UserTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserType",
                table: "Users",
                column: "UserTypes",
                principalTable: "UserType",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_UserType",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypes",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypes",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "UserTypeId",
                table: "Users",
                type: "smallint",
                nullable: true);

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
    }
}
