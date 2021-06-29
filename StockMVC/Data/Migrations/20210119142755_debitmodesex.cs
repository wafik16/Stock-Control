using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class debitmodesex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebitNumber",
                table: "OtherDebits");

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "OtherDebits",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CreateOtherDebitVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebitDate = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    DebitModeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateOtherDebitVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreateOtherDebitVM_DebitModes_DebitModeId",
                        column: x => x.DebitModeId,
                        principalTable: "DebitModes",
                        principalColumn: "DebitModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreateOtherDebitVM_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreateOtherDebitVM_DebitModeId",
                table: "CreateOtherDebitVM",
                column: "DebitModeId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateOtherDebitVM_ProductId",
                table: "CreateOtherDebitVM",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateOtherDebitVM");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OtherDebits");

            migrationBuilder.AddColumn<string>(
                name: "DebitNumber",
                table: "OtherDebits",
                nullable: true);
        }
    }
}
