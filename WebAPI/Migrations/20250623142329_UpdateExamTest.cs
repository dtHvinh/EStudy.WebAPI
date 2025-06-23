using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExamTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TestExams_ExamId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "Questions",
                newName: "SectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_ExamId",
                table: "Questions",
                newName: "IX_Questions_SectionId");

            migrationBuilder.AddColumn<int>(
                name: "AttemptCount",
                table: "TestExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "TestExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "TestExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionCount",
                table: "TestExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TestCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCollections_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestComments_TestExams_TestId",
                        column: x => x.TestId,
                        principalTable: "TestExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestComments_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSections_TestExams_TestId",
                        column: x => x.TestId,
                        principalTable: "TestExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCollections_AuthorId",
                table: "TestCollections",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestComments_AuthorId",
                table: "TestComments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_TestComments_TestId",
                table: "TestComments",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSections_TestId",
                table: "TestSections",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TestSections_SectionId",
                table: "Questions",
                column: "SectionId",
                principalTable: "TestSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TestSections_SectionId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "TestCollections");

            migrationBuilder.DropTable(
                name: "TestComments");

            migrationBuilder.DropTable(
                name: "TestSections");

            migrationBuilder.DropColumn(
                name: "AttemptCount",
                table: "TestExams");

            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "TestExams");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TestExams");

            migrationBuilder.DropColumn(
                name: "QuestionCount",
                table: "TestExams");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "Questions",
                newName: "ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_SectionId",
                table: "Questions",
                newName: "IX_Questions_ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TestExams_ExamId",
                table: "Questions",
                column: "ExamId",
                principalTable: "TestExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
