using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFullTextSearchForFlashCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "FlashCards",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Term", "Definition", "PartOfSpeech" });

            migrationBuilder.CreateIndex(
                name: "IX_FlashCards_SearchVector",
                table: "FlashCards",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FlashCards_SearchVector",
                table: "FlashCards");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "FlashCards");
        }
    }
}
