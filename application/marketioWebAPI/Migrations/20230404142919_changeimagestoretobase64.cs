using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketioWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class changeimagestoretobase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAsBytes",
                table: "ListingImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageAsBase64",
                table: "ListingImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageAsBase64",
                table: "ListingImages");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageAsBytes",
                table: "ListingImages",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
