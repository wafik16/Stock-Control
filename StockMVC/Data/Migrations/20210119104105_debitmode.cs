using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class debitmode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DebitMode",
                columns: table => new
                {
                    DebitModeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModeOfDebit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitMode", x => x.DebitModeId);
                });

            migrationBuilder.CreateTable(
                name: "OtherDebits",
                columns: table => new
                {
                    DebitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebitNumber = table.Column<string>(nullable: true),
                    DebitDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    DebitModeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDebits", x => x.DebitId);
                    table.ForeignKey(
                        name: "FK_OtherDebits_DebitMode_DebitModeId",
                        column: x => x.DebitModeId,
                        principalTable: "DebitMode",
                        principalColumn: "DebitModeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtherDebits_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherDebits_DebitModeId",
                table: "OtherDebits",
                column: "DebitModeId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherDebits_ProductId",
                table: "OtherDebits",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherDebits");

            migrationBuilder.DropTable(
                name: "DebitMode");
        }
    }
}
