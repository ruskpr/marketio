using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketioWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListingTags_Listings_ListingId",
                table: "ListingTags");

            migrationBuilder.DropIndex(
                name: "IX_ListingTags_ListingId",
                table: "ListingTags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ListingTags_ListingId",
                table: "ListingTags",
                column: "ListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListingTags_Listings_ListingId",
                table: "ListingTags",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
