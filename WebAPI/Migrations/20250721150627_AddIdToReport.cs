using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reports",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId_Type_TargetId",
                table: "Reports",
                columns: new[] { "UserId", "Type", "TargetId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_UserId_Type_TargetId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                columns: new[] { "UserId", "Type", "TargetId" });
        }
    }
}
