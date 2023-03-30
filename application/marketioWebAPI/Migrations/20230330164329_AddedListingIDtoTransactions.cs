using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketioWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedListingIDtoTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListingId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ListingId",
                table: "Transactions",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Listings_ListingId",
                table: "Transactions",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Listings_ListingId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ListingId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Transactions");
        }
    }
}
