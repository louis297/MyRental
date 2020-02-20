using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class manual_fk_item_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_items_AspNetUsers_ApplicationUserId",
                table: "items");

            migrationBuilder.DropIndex(
                name: "IX_items_ApplicationUserId",
                table: "items");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "items");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_items_ApplicationUserId",
                table: "items",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_items_AspNetUsers_ApplicationUserId",
                table: "items",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
