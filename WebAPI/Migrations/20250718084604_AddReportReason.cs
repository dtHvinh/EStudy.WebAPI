using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddReportReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportReasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReasons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ReportReasons",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Offensive or adult material.", "Inappropriate Content" },
                    { 2, "Promotes irrelevant or misleading content.", "Spam or Scam" },
                    { 3, "Incorrect or misleading educational information.", "Misinformation" },
                    { 4, "Bullying or abusive behavior.", "Harassment or Abuse" },
                    { 5, "Violates intellectual property rights.", "Copyright Infringement" },
                    { 6, "Promotes harm or violence.", "Violent or Dangerous Content" },
                    { 7, "Copied from another source without citation.", "Plagiarism" },
                    { 8, "Content cannot be accessed or is broken.", "Broken or Unusable Content" },
                    { 9, "Not relevant to the subject matter.", "Off-topic" },
                    { 10, "Other reason not listed.", "Other" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportReasons");
        }
    }
}
