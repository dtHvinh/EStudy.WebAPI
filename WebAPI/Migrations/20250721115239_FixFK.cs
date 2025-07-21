using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizProgress_ChapterQuizzes_ChapterQuizId",
                table: "QuizProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_QuizProgress_CourseLessons_QuizId",
                table: "QuizProgress");

            migrationBuilder.DropIndex(
                name: "IX_QuizProgress_ChapterQuizId",
                table: "QuizProgress");

            migrationBuilder.DropColumn(
                name: "ChapterQuizId",
                table: "QuizProgress");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizProgress_ChapterQuizzes_QuizId",
                table: "QuizProgress",
                column: "QuizId",
                principalTable: "ChapterQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizProgress_ChapterQuizzes_QuizId",
                table: "QuizProgress");

            migrationBuilder.AddColumn<int>(
                name: "ChapterQuizId",
                table: "QuizProgress",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizProgress_ChapterQuizId",
                table: "QuizProgress",
                column: "ChapterQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizProgress_ChapterQuizzes_ChapterQuizId",
                table: "QuizProgress",
                column: "ChapterQuizId",
                principalTable: "ChapterQuizzes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizProgress_CourseLessons_QuizId",
                table: "QuizProgress",
                column: "QuizId",
                principalTable: "CourseLessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
