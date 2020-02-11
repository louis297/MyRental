using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class redesign_itemimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemImages_items_ItemId",
                table: "itemImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itemImages",
                table: "itemImages");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "itemImages");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "itemImages",
                newName: "ItemID");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "itemImages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "itemImages",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageContent",
                table: "itemImages",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ImageType",
                table: "itemImages",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "itemImages",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemImages",
                table: "itemImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_itemImages_ItemID",
                table: "itemImages",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_itemImages_UserID",
                table: "itemImages",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_itemImages_items_ItemID",
                table: "itemImages",
                column: "ItemID",
                principalTable: "items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_itemImages_AspNetUsers_UserID",
                table: "itemImages",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_itemImages_items_ItemID",
                table: "itemImages");

            migrationBuilder.DropForeignKey(
                name: "FK_itemImages_AspNetUsers_UserID",
                table: "itemImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_itemImages",
                table: "itemImages");

            migrationBuilder.DropIndex(
                name: "IX_itemImages_ItemID",
                table: "itemImages");

            migrationBuilder.DropIndex(
                name: "IX_itemImages_UserID",
                table: "itemImages");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "itemImages");

            migrationBuilder.DropColumn(
                name: "ImageContent",
                table: "itemImages");

            migrationBuilder.DropColumn(
                name: "ImageType",
                table: "itemImages");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "itemImages");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "itemImages",
                newName: "ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "ItemId",
                table: "itemImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "itemImages",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_itemImages",
                table: "itemImages",
                columns: new[] { "ItemId", "ImagePath" });

            migrationBuilder.AddForeignKey(
                name: "FK_itemImages_items_ItemId",
                table: "itemImages",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
