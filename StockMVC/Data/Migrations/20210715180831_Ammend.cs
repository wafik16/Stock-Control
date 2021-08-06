using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class Ammend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "NewStockLists",
                nullable: true);

            migrationBuilder.InsertData(
                table: "DebitModes",
                columns: new[] { "DebitModeId", "ModeOfDebit" },
                values: new object[,]
                {
                    { 1, "DAMAGE" },
                    { 2, "PR" },
                    { 3, "WEIGH LOSS" },
                    { 4, "WEIGHT GAIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DebitModes",
                keyColumn: "DebitModeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DebitModes",
                keyColumn: "DebitModeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DebitModes",
                keyColumn: "DebitModeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DebitModes",
                keyColumn: "DebitModeId",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NewStockLists");
        }
    }
}
