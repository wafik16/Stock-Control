using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class AddPaymentModeRepo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesViewModel");

            migrationBuilder.RenameColumn(
                name: "PaymentMode",
                table: "orders",
                newName: "PaymentModeId");

            migrationBuilder.CreateTable(
                name: "PaymentMode",
                columns: table => new
                {
                    PaymentModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModeOfPayment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMode", x => x.PaymentModeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_PaymentModeId",
                table: "orders",
                column: "PaymentModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_PaymentMode_PaymentModeId",
                table: "orders",
                column: "PaymentModeId",
                principalTable: "PaymentMode",
                principalColumn: "PaymentModeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_PaymentMode_PaymentModeId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "PaymentMode");

            migrationBuilder.DropIndex(
                name: "IX_orders_PaymentModeId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "PaymentModeId",
                table: "orders",
                newName: "PaymentMode");

            migrationBuilder.CreateTable(
                name: "SalesViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: true),
                    OrderedItemOrderId = table.Column<int>(nullable: true),
                    OrderedItemProductId = table.Column<int>(nullable: true),
                    PaymentMode = table.Column<int>(nullable: false),
                    ProductCategoryCategoryId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    StaffID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesViewModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesViewModel_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesViewModel_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesViewModel_ProductCategory_ProductCategoryCategoryId",
                        column: x => x.ProductCategoryCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesViewModel_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesViewModel_Staff_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesViewModel_OrderedItem_OrderedItemOrderId_OrderedItemProductId",
                        columns: x => new { x.OrderedItemOrderId, x.OrderedItemProductId },
                        principalTable: "OrderedItem",
                        principalColumns: new[] { "OrderId", "ProductId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesViewModel_CustomerID",
                table: "SalesViewModel",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesViewModel_OrderId",
                table: "SalesViewModel",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesViewModel_ProductCategoryCategoryId",
                table: "SalesViewModel",
                column: "ProductCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesViewModel_ProductId",
                table: "SalesViewModel",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesViewModel_StaffID",
                table: "SalesViewModel",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesViewModel_OrderedItemOrderId_OrderedItemProductId",
                table: "SalesViewModel",
                columns: new[] { "OrderedItemOrderId", "OrderedItemProductId" });
        }
    }
}
