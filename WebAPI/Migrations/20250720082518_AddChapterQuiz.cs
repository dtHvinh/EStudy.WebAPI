using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddChapterQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChapterQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    ChapterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterQuizzes_CourseChapters_ChapterId",
                        column: x => x.ChapterId,
                        principalTable: "CourseChapters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChapterQuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionText = table.Column<string>(type: "text", nullable: false),
                    ChapterQuizId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterQuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterQuizQuestions_ChapterQuizzes_ChapterQuizId",
                        column: x => x.ChapterQuizId,
                        principalTable: "ChapterQuizzes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChapterQuizQuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    ChapterQuizQuestionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapterQuizQuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChapterQuizQuestionOptions_ChapterQuizQuestions_ChapterQuiz~",
                        column: x => x.ChapterQuizQuestionId,
                        principalTable: "ChapterQuizQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChapterQuizQuestionOptions_ChapterQuizQuestionId",
                table: "ChapterQuizQuestionOptions",
                column: "ChapterQuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterQuizQuestions_ChapterQuizId",
                table: "ChapterQuizQuestions",
                column: "ChapterQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterQuizzes_ChapterId",
                table: "ChapterQuizzes",
                column: "ChapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChapterQuizQuestionOptions");

            migrationBuilder.DropTable(
                name: "ChapterQuizQuestions");

            migrationBuilder.DropTable(
                name: "ChapterQuizzes");
        }
    }
}
