using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSelfRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Questions_ParentQuestionId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestComments_TestComments_ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropIndex(
                name: "IX_TestComments_ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ParentQuestionId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropColumn(
                name: "ParentQuestionId",
                table: "Questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "TestComments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentQuestionId",
                table: "Questions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestComments_ParentCommentId",
                table: "TestComments",
                column: "ParentCommentId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TestComments_TestComments_ParentCommentId",
                table: "TestComments",
                column: "ParentCommentId",
                principalTable: "TestComments",
                principalColumn: "Id");
        }
    }
}
