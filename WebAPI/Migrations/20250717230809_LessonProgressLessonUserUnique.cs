using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class LessonProgressLessonUserUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LessonProgress_LessonId",
                table: "LessonProgress");

            migrationBuilder.DropColumn(
                name: "LastWatchedDate",
                table: "LessonProgress");

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgress_LessonId_UserId",
                table: "LessonProgress",
                columns: new[] { "LessonId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LessonProgress_LessonId_UserId",
                table: "LessonProgress");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastWatchedDate",
                table: "LessonProgress",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonProgress_LessonId",
                table: "LessonProgress",
                column: "LessonId");
        }
    }
}
