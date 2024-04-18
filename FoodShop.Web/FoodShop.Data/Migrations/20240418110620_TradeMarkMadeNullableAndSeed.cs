using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodShop.Data.Migrations
{
    public partial class TradeMarkMadeNullableAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TradeMarkId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Milk Products" },
                    { 2, "Bread" },
                    { 3, "Meat" },
                    { 4, "Fruits" },
                    { 5, "Vegetables" },
                    { 6, "Eggs" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Organic" },
                    { 3, "Promo" }
                });

            migrationBuilder.InsertData(
                table: "TradeMarks",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Vereya" },
                    { 2, "Sayana" },
                    { 3, "Mio" },
                    { 4, "Simid" },
                    { 5, "Horisont" },
                    { 6, "Bagryanka" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "PictureUrl", "Price", "ProductTypeId", "Quantity", "TradeMarkId" },
                values: new object[,]
                {
                    { 1, 2, "White sliced bread, prepared from wheat flour.", "Bread", "https://cdncloudcart.com/16398/products/images/50027/hlab-mio-bal-narazan-650-gr-image_63bfea2451a4c_600x600.png?1673521707", 1.60m, 1, 35, 3 },
                    { 2, 1, "Cow milk", "Milk", "https://pingo.bg/Content/products/124/800-800.jpg", 3.70m, 1, 30, 1 },
                    { 3, 3, "Pork Ham", "Ham", "https://www.wenthere8this.com/wp-content/uploads/2021/10/sous-vide-ham-6.jpg", 8.90m, 1, 20, null },
                    { 4, 4, "Green Apples", "Apples", "https://letsgetittraining.com/wp-content/uploads/2019/01/Green-Apples-1.jpg", 3.90m, 2, 40, null },
                    { 5, 5, "Tomatoes", "Tomatoes", "https://www.liveeatlearn.com/wp-content/uploads/2019/05/common-types-of-tomatoes-20.jpg", 2.90m, 3, 60, null },
                    { 6, 6, "Organic Eggs", "Eggs", "https://www.ecowatch.com/wp-content/uploads/2021/09/1893442151-img.jpg", 6.90m, 2, 20, 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TradeMarks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TradeMarks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TradeMarks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TradeMarks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TradeMarks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TradeMarks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "TradeMarkId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
