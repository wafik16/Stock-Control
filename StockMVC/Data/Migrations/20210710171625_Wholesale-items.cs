using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class Wholesaleitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    DeliveryFee = table.Column<decimal>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PaymentModeId = table.Column<int>(nullable: false),
                    TotalWithoutVat = table.Column<decimal>(nullable: false),
                    TotalVat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_WholesaleOrders_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WholesaleOrders_paymentModes_PaymentModeId",
                        column: x => x.PaymentModeId,
                        principalTable: "paymentModes",
                        principalColumn: "PaymentModeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleOrderedItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleOrderedItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WholesaleOrderedItems_WholesaleOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "WholesaleOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WholesaleOrderedItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleOrderedItems_ProductId",
                table: "WholesaleOrderedItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleOrders_CustomerId",
                table: "WholesaleOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleOrders_PaymentModeId",
                table: "WholesaleOrders",
                column: "PaymentModeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleOrderedItems");

            migrationBuilder.DropTable(
                name: "WholesaleOrders");
        }
    }
}
