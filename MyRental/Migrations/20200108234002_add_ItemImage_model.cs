using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class add_ItemImage_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "itemImages",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemImages", x => new { x.ItemId, x.ImagePath });
                    table.ForeignKey(
                        name: "FK_itemImages_items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "items",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemImages");
        }
    }
}
