using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class AddPaymentMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMode",
                table: "orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMode",
                table: "orders");
        }
    }
}
