using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlashCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Front",
                table: "FlashCards",
                newName: "Term");

            migrationBuilder.RenameColumn(
                name: "Back",
                table: "FlashCards",
                newName: "Definition");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Term",
                table: "FlashCards",
                newName: "Front");

            migrationBuilder.RenameColumn(
                name: "Definition",
                table: "FlashCards",
                newName: "Back");
        }
    }
}
