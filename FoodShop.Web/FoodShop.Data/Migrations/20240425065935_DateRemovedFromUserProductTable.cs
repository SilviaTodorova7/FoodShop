using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodShop.Data.Migrations
{
    public partial class DateRemovedFromUserProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 24, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
