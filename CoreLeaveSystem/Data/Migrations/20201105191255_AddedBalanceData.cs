using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLeaveSystem.Data.Migrations
{
    public partial class AddedBalanceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacationTypeVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypeVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationTypeVM");
        }
    }
}
