using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibraryAPI.Library.Migrations
{
    /// <inheritdoc />
    public partial class AddCopyToBookMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CopyID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CopyID",
                table: "Books",
                column: "CopyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Copies_CopyID",
                table: "Books",
                column: "CopyID",
                principalTable: "Copies",
                principalColumn: "CopyID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Copies_CopyID",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CopyID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CopyID",
                table: "Books");
        }
    }
}
