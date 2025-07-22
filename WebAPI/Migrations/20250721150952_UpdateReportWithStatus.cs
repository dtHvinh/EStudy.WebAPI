using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportWithStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportStatusId",
                table: "Reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportStatusId",
                table: "Reports",
                column: "ReportStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_ReportStatuses_ReportStatusId",
                table: "Reports",
                column: "ReportStatusId",
                principalTable: "ReportStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_ReportStatuses_ReportStatusId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ReportStatusId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ReportStatusId",
                table: "Reports");
        }
    }
}
