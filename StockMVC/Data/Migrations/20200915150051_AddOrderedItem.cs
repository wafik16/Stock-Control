using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class AddOrderedItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryFee",
                table: "orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "OrderedItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "SalesViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerID = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    ProductCategoryCategoryId = table.Column<int>(nullable: true),
                    PaymentMode = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    OrderedItemOrderId = table.Column<int>(nullable: true),
                    OrderedItemProductId = table.Column<int>(nullable: true),
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesViewModel");

            migrationBuilder.DropColumn(
                name: "DeliveryFee",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderedItem");
        }
    }
}
