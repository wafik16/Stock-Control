using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class newinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Invoice",
                table: "orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "invoiceNumbers",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false),
                    InvoiceNumbers = table.Column<string>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_invoiceNumbers_Invoice",
                table: "orders");

            migrationBuilder.DropTable(
                name: "invoiceNumbers");

            migrationBuilder.DropIndex(
                name: "IX_orders_Invoice",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "orders");
        }
    }
}
