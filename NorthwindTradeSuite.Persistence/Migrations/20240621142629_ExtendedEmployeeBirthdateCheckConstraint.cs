using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindTradeSuite.Persistence.Migrations
{
    public partial class ExtendedEmployeeBirthdateCheckConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Employee_BirthDate",
                table: "Employees");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employee_BirthDate",
                table: "Employees",
                sql: "CreatedAt IS NULL OR BirthDate < CreatedAt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Employee_BirthDate",
                table: "Employees");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employee_BirthDate",
                table: "Employees",
                sql: "BirthDate > CreatedAt");
        }
    }
}
