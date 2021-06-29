using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class ChangesOnOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Customer_CustomerId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_CustomerId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "orders");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderedItem",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderedItem");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Customer_CustomerId",
                table: "orders",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
