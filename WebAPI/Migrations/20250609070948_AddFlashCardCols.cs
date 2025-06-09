using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFlashCardCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Example",
                table: "FlashCards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "FlashCards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FlashCards",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PartOfSpeech",
                table: "FlashCards",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Example",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "PartOfSpeech",
                table: "FlashCards");
        }
    }
}
