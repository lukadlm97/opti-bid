using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiBid.Microservices.Auction.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedcustomerandauctionassetsrelations202325012241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerID",
                table: "AuctionAssets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionAssets_CustomerID",
                table: "AuctionAssets",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionAssets_Customers_CustomerID",
                table: "AuctionAssets",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionAssets_Customers_CustomerID",
                table: "AuctionAssets");

            migrationBuilder.DropIndex(
                name: "IX_AuctionAssets_CustomerID",
                table: "AuctionAssets");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "AuctionAssets");
        }
    }
}
