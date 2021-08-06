using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class SeedPaymentMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "paymentModes",
                columns: new[] { "PaymentModeId", "ModeOfPayment" },
                values: new object[,]
                {
                    { 1, "CASH" },
                    { 2, "POS" },
                    { 3, "TRANSFER" },
                    { 4, "CREDIT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "paymentModes",
                keyColumn: "PaymentModeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "paymentModes",
                keyColumn: "PaymentModeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "paymentModes",
                keyColumn: "PaymentModeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "paymentModes",
                keyColumn: "PaymentModeId",
                keyValue: 4);
        }
    }
}
