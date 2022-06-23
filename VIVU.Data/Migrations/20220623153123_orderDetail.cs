using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIVU.Data.Migrations
{
    public partial class orderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductSku",
                table: "SalesOrderDetails",
                newName: "ProductName");

            migrationBuilder.AddColumn<decimal>(
                name: "DeliverPrice",
                table: "SalesOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SalesOrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SalesOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "SalesOrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "SalesOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "SalesOrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliverPrice",
                table: "SalesOrderDetails");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SalesOrderDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SalesOrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SalesOrderDetails");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SalesOrderDetails");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SalesOrderDetails");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "SalesOrderDetails",
                newName: "ProductSku");
        }
    }
}
