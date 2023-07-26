using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class DeleteInUserTypeClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserType",
                keyColumn: "UserTypeId",
                keyValue: (short)2);

            migrationBuilder.UpdateData(
                table: "UserType",
                keyColumn: "UserTypeId",
                keyValue: (short)1,
                column: "UserTypeName",
                value: "Manager");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserType",
                keyColumn: "UserTypeId",
                keyValue: (short)1,
                column: "UserTypeName",
                value: "Client");

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "UserTypeId", "UserTypeName" },
                values: new object[] { (short)2, "Manager" });
        }
    }
}
