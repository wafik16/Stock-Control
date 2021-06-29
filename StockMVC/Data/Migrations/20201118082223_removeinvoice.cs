using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class removeinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_invoiceNumbers_Invoice",
                table: "orders");

            migrationBuilder.DropTable(
                name: "invoiceNumbers");

            migrationBuilder.DropIndex(
                name: "IX_orders_Invoice",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Invoice",
                table: "orders",
                newName: "InvoiceNumber");

            migrationBuilder.AlterColumn<string>(
                name: "InvoiceNumber",
                table: "orders",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "orders",
                newName: "Invoice");

            migrationBuilder.AlterColumn<string>(
                name: "Invoice",
                table: "orders",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "invoiceNumbers",
                columns: table => new
                {
                    InvoiceNumbers = table.Column<string>(nullable: false),
                    InvoiceId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoiceNumbers", x => x.InvoiceNumbers);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_Invoice",
                table: "orders",
                column: "Invoice");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_invoiceNumbers_Invoice",
                table: "orders",
                column: "Invoice",
                principalTable: "invoiceNumbers",
                principalColumn: "InvoiceNumbers",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
