using Microsoft.EntityFrameworkCore.Migrations;

namespace ShiftTrackingAPI.Migrations
{
    public partial class AddColumnIsViolationToTableShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsViolation",
                table: "shifts",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsViolation",
                table: "shifts");
        }
    }
}
