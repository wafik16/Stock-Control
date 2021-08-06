using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class _13072021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WholesalePrices_ProductId",
                table: "WholesalePrices",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePrices_Customer_CustomerId",
                table: "WholesalePrices",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePrices_Product_ProductId",
                table: "WholesalePrices",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePrices_Customer_CustomerId",
                table: "WholesalePrices");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePrices_Product_ProductId",
                table: "WholesalePrices");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePrices_ProductId",
                table: "WholesalePrices");
        }
    }
}
