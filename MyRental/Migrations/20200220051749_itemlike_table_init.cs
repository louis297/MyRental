using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class itemlike_table_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "itemLikes",
                columns: table => new
                {
                    ItemLikedID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemLikes", x => new { x.UserID, x.ItemLikedID });
                    table.ForeignKey(
                        name: "FK_itemLikes_items_ItemLikedID",
                        column: x => x.ItemLikedID,
                        principalTable: "items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemLikes_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_itemLikes_ItemLikedID",
                table: "itemLikes",
                column: "ItemLikedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemLikes");
        }
    }
}
