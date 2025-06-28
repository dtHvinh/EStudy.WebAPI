using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "TestExams");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TestSections",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TestSections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TestExams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassingScore",
                table: "TestExams",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentQuestionId",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ParentQuestionId",
                table: "Questions",
                column: "ParentQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Questions_ParentQuestionId",
                table: "Questions",
                column: "ParentQuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_ParentQuestionId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ParentQuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TestSections");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "TestExams");

            migrationBuilder.DropColumn(
                name: "PassingScore",
                table: "TestExams");

            migrationBuilder.DropColumn(
                name: "ParentQuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TestSections",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "TestExams",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
