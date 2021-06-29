using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class debitmodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherDebits_DebitMode_DebitModeId",
                table: "OtherDebits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebitMode",
                table: "DebitMode");

            migrationBuilder.RenameTable(
                name: "DebitMode",
                newName: "DebitModes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebitModes",
                table: "DebitModes",
                column: "DebitModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherDebits_DebitModes_DebitModeId",
                table: "OtherDebits",
                column: "DebitModeId",
                principalTable: "DebitModes",
                principalColumn: "DebitModeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherDebits_DebitModes_DebitModeId",
                table: "OtherDebits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebitModes",
                table: "DebitModes");

            migrationBuilder.RenameTable(
                name: "DebitModes",
                newName: "DebitMode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebitMode",
                table: "DebitMode",
                column: "DebitModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherDebits_DebitMode_DebitModeId",
                table: "OtherDebits",
                column: "DebitModeId",
                principalTable: "DebitMode",
                principalColumn: "DebitModeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
