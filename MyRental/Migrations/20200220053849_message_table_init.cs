using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRental.Migrations
{
    public partial class message_table_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyProperty",
                columns: table => new
                {
                    MessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: true),
                    Content = table.Column<string>(maxLength: 2000, nullable: true),
                    SentTime = table.Column<DateTime>(nullable: false),
                    SenderID = table.Column<string>(nullable: true),
                    ReceiverID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProperty", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_MyProperty_AspNetUsers_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyProperty_AspNetUsers_SenderID",
                        column: x => x.SenderID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyProperty_ReceiverID",
                table: "MyProperty",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_MyProperty_SenderID",
                table: "MyProperty",
                column: "SenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyProperty");
        }
    }
}
