using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddedToUserManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO USERS (FirstName, LastName, UserTypes, DepartmentID)
            VALUES ('John', 'Doe', 1, 18); ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
