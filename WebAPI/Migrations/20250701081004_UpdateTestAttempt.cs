using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTestAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "TestAttempts");

            migrationBuilder.AddColumn<int>(
                name: "TimeSpent",
                table: "TestAttempts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpent",
                table: "TestAttempts");

            migrationBuilder.AddColumn<double>(
                name: "Score",
                table: "TestAttempts",
                type: "double precision",
                nullable: true);
        }
    }
}
