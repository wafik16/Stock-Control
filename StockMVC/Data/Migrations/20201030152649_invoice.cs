using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_Staff_StaffId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_StaffId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "orders",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_StaffId",
                table: "orders",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Staff_StaffId",
                table: "orders",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
