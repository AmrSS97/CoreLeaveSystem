using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLeaveSystem.Data.Migrations
{
    public partial class AddedPeriodAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "VacationAllocations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "VacationAllocations");
        }
    }
}
