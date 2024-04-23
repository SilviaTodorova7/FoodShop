using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodShop.Data.Migrations
{
    public partial class AddedIsActiveColumnOfProductEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "UserProducts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 0, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
