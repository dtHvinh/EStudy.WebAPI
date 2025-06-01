using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_InstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardSets_Users_UserId",
                table: "FlashCardSets");

            migrationBuilder.DropForeignKey(
                name: "FK_StudySchedules_Users_UserId",
                table: "StudySchedules");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StudySchedules",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_StudySchedules_UserId",
                table: "StudySchedules",
                newName: "IX_StudySchedules_AuthorId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FlashCardSets",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCardSets_UserId",
                table: "FlashCardSets",
                newName: "IX_FlashCardSets_AuthorId");

            migrationBuilder.RenameColumn(
                name: "InstructorId",
                table: "Courses",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_InstructorId",
                table: "Courses",
                newName: "IX_Courses_AuthorId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ModificationDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_AuthorId",
                table: "Courses",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardSets_Users_AuthorId",
                table: "FlashCardSets",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySchedules_Users_AuthorId",
                table: "StudySchedules",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_AuthorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashCardSets_Users_AuthorId",
                table: "FlashCardSets");

            migrationBuilder.DropForeignKey(
                name: "FK_StudySchedules_Users_AuthorId",
                table: "StudySchedules");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "StudySchedules",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StudySchedules_AuthorId",
                table: "StudySchedules",
                newName: "IX_StudySchedules_UserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "FlashCardSets",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCardSets_AuthorId",
                table: "FlashCardSets",
                newName: "IX_FlashCardSets_UserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Courses",
                newName: "InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                newName: "IX_Courses_InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_InstructorId",
                table: "Courses",
                column: "InstructorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCardSets_Users_UserId",
                table: "FlashCardSets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudySchedules_Users_UserId",
                table: "StudySchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
