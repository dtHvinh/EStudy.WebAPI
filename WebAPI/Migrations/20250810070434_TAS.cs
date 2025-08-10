using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class TAS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestAnswerSellections");

            migrationBuilder.CreateTable(
                name: "TestAnswerSelections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestAttemptId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SelectedAnswerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswerSelections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswerSelections_Answers_SelectedAnswerId",
                        column: x => x.SelectedAnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAnswerSelections_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAnswerSelections_TestAttempts_TestAttemptId",
                        column: x => x.TestAttemptId,
                        principalTable: "TestAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerSelections_QuestionId",
                table: "TestAnswerSelections",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerSelections_SelectedAnswerId",
                table: "TestAnswerSelections",
                column: "SelectedAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerSelections_TestAttemptId",
                table: "TestAnswerSelections",
                column: "TestAttemptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestAnswerSelections");

            migrationBuilder.CreateTable(
                name: "TestAnswerSellections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SelectedAnswerId = table.Column<int>(type: "integer", nullable: false),
                    TestAttemptId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAnswerSellections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestAnswerSellections_Answers_SelectedAnswerId",
                        column: x => x.SelectedAnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAnswerSellections_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestAnswerSellections_TestAttempts_TestAttemptId",
                        column: x => x.TestAttemptId,
                        principalTable: "TestAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerSellections_QuestionId",
                table: "TestAnswerSellections",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerSellections_SelectedAnswerId",
                table: "TestAnswerSellections",
                column: "SelectedAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestAnswerSellections_TestAttemptId",
                table: "TestAnswerSellections",
                column: "TestAttemptId");
        }
    }
}
