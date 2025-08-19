using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingPOC.Migrations
{
    /// <inheritdoc />
    public partial class AddProductoPurchaseDetailRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetail_ProductId",
                table: "PurchaseDetail",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetail_Products_ProductId",
                table: "PurchaseDetail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetail_Products_ProductId",
                table: "PurchaseDetail");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseDetail_ProductId",
                table: "PurchaseDetail");
        }
    }
}
