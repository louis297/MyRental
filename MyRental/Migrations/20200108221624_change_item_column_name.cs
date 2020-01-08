using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class change_item_column_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "items");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "items",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "ItemID",
                keyValue: 1,
                column: "ItemName",
                value: "item1");

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "ItemID",
                keyValue: 2,
                column: "ItemName",
                value: "item2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "items");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "items",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "ItemID",
                keyValue: 1,
                column: "Title",
                value: "item1");

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "ItemID",
                keyValue: 2,
                column: "Title",
                value: "item2");
        }
    }
}
