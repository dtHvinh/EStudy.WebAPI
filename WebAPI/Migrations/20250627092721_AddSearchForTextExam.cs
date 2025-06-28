using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchForTextExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "TestExams",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title" });

            migrationBuilder.CreateIndex(
                name: "IX_TestExams_SearchVector",
                table: "TestExams",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestExams_SearchVector",
                table: "TestExams");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "TestExams");
        }
    }
}
