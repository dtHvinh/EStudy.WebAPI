using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCorrectConstrait : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChapterQuizQuestionOptions_ChapterQuizQuestions_ChapterQuiz~",
                table: "ChapterQuizQuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChapterQuizQuestions_ChapterQuizzes_ChapterQuizId",
                table: "ChapterQuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ChapterQuizQuestions_ChapterQuizId",
                table: "ChapterQuizQuestions");

            migrationBuilder.DropColumn(
                name: "ChapterQuizId",
                table: "ChapterQuizQuestions");

            migrationBuilder.AddColumn<int>(
                name: "ChapterId",
                table: "ChapterQuizQuestions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterQuizQuestionId",
                table: "ChapterQuizQuestionOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChapterQuizQuestions_ChapterId",
                table: "ChapterQuizQuestions",
                column: "ChapterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterQuizQuestionOptions_ChapterQuizQuestions_ChapterQuiz~",
                table: "ChapterQuizQuestionOptions",
                column: "ChapterQuizQuestionId",
                principalTable: "ChapterQuizQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterQuizQuestions_ChapterQuizzes_ChapterId",
                table: "ChapterQuizQuestions",
                column: "ChapterId",
                principalTable: "ChapterQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChapterQuizQuestionOptions_ChapterQuizQuestions_ChapterQuiz~",
                table: "ChapterQuizQuestionOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ChapterQuizQuestions_ChapterQuizzes_ChapterId",
                table: "ChapterQuizQuestions");

            migrationBuilder.DropIndex(
                name: "IX_ChapterQuizQuestions_ChapterId",
                table: "ChapterQuizQuestions");

            migrationBuilder.DropColumn(
                name: "ChapterId",
                table: "ChapterQuizQuestions");

            migrationBuilder.AddColumn<int>(
                name: "ChapterQuizId",
                table: "ChapterQuizQuestions",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChapterQuizQuestionId",
                table: "ChapterQuizQuestionOptions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_ChapterQuizQuestions_ChapterQuizId",
                table: "ChapterQuizQuestions",
                column: "ChapterQuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterQuizQuestionOptions_ChapterQuizQuestions_ChapterQuiz~",
                table: "ChapterQuizQuestionOptions",
                column: "ChapterQuizQuestionId",
                principalTable: "ChapterQuizQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChapterQuizQuestions_ChapterQuizzes_ChapterQuizId",
                table: "ChapterQuizQuestions",
                column: "ChapterQuizId",
                principalTable: "ChapterQuizzes",
                principalColumn: "Id");
        }
    }
}
