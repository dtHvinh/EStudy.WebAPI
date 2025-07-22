using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTestCollectionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestCollectionTestExam",
                columns: table => new
                {
                    CollectionsId = table.Column<int>(type: "integer", nullable: false),
                    TestsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCollectionTestExam", x => new { x.CollectionsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_TestCollectionTestExam_TestCollections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "TestCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestCollectionTestExam_TestExams_TestsId",
                        column: x => x.TestsId,
                        principalTable: "TestExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestCollectionTestExam_TestsId",
                table: "TestCollectionTestExam",
                column: "TestsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestCollectionTestExam");
        }
    }
}
