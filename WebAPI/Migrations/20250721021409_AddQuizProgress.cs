using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompletionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ChapterQuizId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizProgress_ChapterQuizzes_ChapterQuizId",
                        column: x => x.ChapterQuizId,
                        principalTable: "ChapterQuizzes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuizProgress_CourseLessons_QuizId",
                        column: x => x.QuizId,
                        principalTable: "CourseLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizProgress_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizProgress_ChapterQuizId",
                table: "QuizProgress",
                column: "ChapterQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizProgress_QuizId_UserId",
                table: "QuizProgress",
                columns: new[] { "QuizId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizProgress_UserId",
                table: "QuizProgress",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizProgress");
        }
    }
}
