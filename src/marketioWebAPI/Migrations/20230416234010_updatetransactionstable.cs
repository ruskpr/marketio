using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marketioWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatetransactionstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCompleted",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DateCompleted",
                table: "Transactions");
        }
    }
}
