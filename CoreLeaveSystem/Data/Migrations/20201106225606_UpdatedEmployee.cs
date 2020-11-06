using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreLeaveSystem.Data.Migrations
{
    public partial class UpdatedEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "VacationTypes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datejoined",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Fullname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EditVacationAllocationVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(nullable: true),
                    NumberOfDays = table.Column<int>(nullable: false),
                    vacationTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditVacationAllocationVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EditVacationAllocationVM_EmployeeVM_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EditVacationAllocationVM_VacationTypeVM_vacationTypeId",
                        column: x => x.vacationTypeId,
                        principalTable: "VacationTypeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VacationAllocationVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    VacationTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationAllocationVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationAllocationVM_EmployeeVM_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VacationAllocationVM_VacationTypeVM_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EditVacationAllocationVM_EmployeeId",
                table: "EditVacationAllocationVM",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EditVacationAllocationVM_vacationTypeId",
                table: "EditVacationAllocationVM",
                column: "vacationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationAllocationVM_EmployeeId",
                table: "VacationAllocationVM",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationAllocationVM_VacationTypeId",
                table: "VacationAllocationVM",
                column: "VacationTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EditVacationAllocationVM");

            migrationBuilder.DropTable(
                name: "VacationAllocationVM");

            migrationBuilder.DropTable(
                name: "EmployeeVM");

            migrationBuilder.DropColumn(
                name: "Datejoined",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "VacationTypes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
