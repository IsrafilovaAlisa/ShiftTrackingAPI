using Microsoft.EntityFrameworkCore.Migrations;

namespace ShiftTrackingAPI.Migrations
{
    public partial class ChangedColumnNameWorkTimeHoursFromTableShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkTime",
                table: "shifts",
                newName: "WorkTimeHours");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkTimeHours",
                table: "shifts",
                newName: "WorkTime");
        }
    }
}
