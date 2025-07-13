using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WatchPercentage",
                table: "LessonProgress");

            migrationBuilder.DropColumn(
                name: "WatchTimeSeconds",
                table: "LessonProgress");

            migrationBuilder.CreateTable(
                name: "UserCourseNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourseNotes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseNotes_CourseId",
                table: "UserCourseNotes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseNotes_UserId",
                table: "UserCourseNotes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourseNotes");

            migrationBuilder.AddColumn<int>(
                name: "WatchPercentage",
                table: "LessonProgress",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WatchTimeSeconds",
                table: "LessonProgress",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
