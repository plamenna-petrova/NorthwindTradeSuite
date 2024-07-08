using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindTradeSuite.Persistence.Migrations
{
    public partial class EntitiesAuditInfoUserIdReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Territories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Territories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Territories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Suppliers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Shippers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Shippers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Shippers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Shippers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Shippers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Shippers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Categories");
        }
    }
}
