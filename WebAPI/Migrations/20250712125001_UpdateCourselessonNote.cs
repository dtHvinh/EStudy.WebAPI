using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCourselessonNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCourseNotes");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "LessonProgress");

            migrationBuilder.CreateTable(
                name: "UserLessonNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLessonNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLessonNotes_CourseLessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "CourseLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLessonNotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLessonNotes_LessonId",
                table: "UserLessonNotes",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLessonNotes_UserId",
                table: "UserLessonNotes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLessonNotes");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "LessonProgress",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UserCourseNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModificationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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
    }
}
