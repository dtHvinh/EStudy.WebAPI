using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSelfRefForComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentCommentId",
                table: "TestComments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestComments_ParentCommentId",
                table: "TestComments",
                column: "ParentCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestComments_TestComments_ParentCommentId",
                table: "TestComments",
                column: "ParentCommentId",
                principalTable: "TestComments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestComments_TestComments_ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropIndex(
                name: "IX_TestComments_ParentCommentId",
                table: "TestComments");

            migrationBuilder.DropColumn(
                name: "ParentCommentId",
                table: "TestComments");
        }
    }
}
