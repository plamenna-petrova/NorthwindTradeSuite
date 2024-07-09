using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NorthwindTradeSuite.Persistence.Migrations
{
    public partial class RemovedModifiedAtToDeletedAtCheckConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Territory_DeletedAt",
                table: "Territories");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Supplier_DeletedAt",
                table: "Suppliers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Shipper_DeletedAt",
                table: "Shippers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Region_DeletedAt",
                table: "Regions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Product_DeletedAt",
                table: "Products");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_DeletedAt",
                table: "Orders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Employee_DeletedAt",
                table: "Employees");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Customer_DeletedAt",
                table: "Customers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Category_DeletedAt",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Territory_DeletedAt",
                table: "Territories",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Supplier_DeletedAt",
                table: "Suppliers",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Shipper_DeletedAt",
                table: "Shippers",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Region_DeletedAt",
                table: "Regions",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Product_DeletedAt",
                table: "Products",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_DeletedAt",
                table: "Orders",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employee_DeletedAt",
                table: "Employees",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Customer_DeletedAt",
                table: "Customers",
                sql: "DeletedAt >= ModifiedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Category_DeletedAt",
                table: "Categories",
                sql: "DeletedAt >= ModifiedAt");
        }
    }
}
