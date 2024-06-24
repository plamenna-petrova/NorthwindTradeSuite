using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindTradeSuite.Persistence.Migrations
{
    public partial class ProductUnitsOnOrderColumnRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitsInOrder",
                table: "Products",
                newName: "UnitsOnOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitsOnOrder",
                table: "Products",
                newName: "UnitsInOrder");
        }
    }
}
