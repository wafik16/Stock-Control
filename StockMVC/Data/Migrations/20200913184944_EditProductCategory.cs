using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMVC.Data.Migrations
{
    public partial class EditProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_{ProductCategory_CategoryId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_{ProductCategory",
                table: "{ProductCategory");

            migrationBuilder.RenameTable(
                name: "{ProductCategory",
                newName: "ProductCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategory_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "ProductCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategory_CategoryId",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                newName: "{ProductCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_{ProductCategory",
                table: "{ProductCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_{ProductCategory_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "{ProductCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
