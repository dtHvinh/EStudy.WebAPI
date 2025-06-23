using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchForBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Blogs",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Content" });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_SearchVector",
                table: "Blogs",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blogs_SearchVector",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Blogs");
        }
    }
}
