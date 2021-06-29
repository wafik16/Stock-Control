using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_PaymentMode_PaymentModeId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMode",
                table: "PaymentMode");

            migrationBuilder.RenameTable(
                name: "PaymentMode",
                newName: "paymentModes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymentModes",
                table: "paymentModes",
                column: "PaymentModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_paymentModes_PaymentModeId",
                table: "orders",
                column: "PaymentModeId",
                principalTable: "paymentModes",
                principalColumn: "PaymentModeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_paymentModes_PaymentModeId",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymentModes",
                table: "paymentModes");

            migrationBuilder.RenameTable(
                name: "paymentModes",
                newName: "PaymentMode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMode",
                table: "PaymentMode",
                column: "PaymentModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_PaymentMode_PaymentModeId",
                table: "orders",
                column: "PaymentModeId",
                principalTable: "PaymentMode",
                principalColumn: "PaymentModeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
