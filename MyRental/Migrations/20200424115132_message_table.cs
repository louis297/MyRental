using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class message_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_items_ItemID",
                table: "MyProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_AspNetUsers_ReceiverID",
                table: "MyProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_MyProperty_AspNetUsers_SenderID",
                table: "MyProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "messages");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_SenderID",
                table: "messages",
                newName: "IX_messages_SenderID");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_ReceiverID",
                table: "messages",
                newName: "IX_messages_ReceiverID");

            migrationBuilder.RenameIndex(
                name: "IX_MyProperty_ItemID",
                table: "messages",
                newName: "IX_messages_ItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "MessageID");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_items_ItemID",
                table: "messages",
                column: "ItemID",
                principalTable: "items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_ReceiverID",
                table: "messages",
                column: "ReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_AspNetUsers_SenderID",
                table: "messages",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_items_ItemID",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_ReceiverID",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_AspNetUsers_SenderID",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "MyProperty");

            migrationBuilder.RenameIndex(
                name: "IX_messages_SenderID",
                table: "MyProperty",
                newName: "IX_MyProperty_SenderID");

            migrationBuilder.RenameIndex(
                name: "IX_messages_ReceiverID",
                table: "MyProperty",
                newName: "IX_MyProperty_ReceiverID");

            migrationBuilder.RenameIndex(
                name: "IX_messages_ItemID",
                table: "MyProperty",
                newName: "IX_MyProperty_ItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "MessageID");

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_items_ItemID",
                table: "MyProperty",
                column: "ItemID",
                principalTable: "items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_AspNetUsers_ReceiverID",
                table: "MyProperty",
                column: "ReceiverID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MyProperty_AspNetUsers_SenderID",
                table: "MyProperty",
                column: "SenderID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
