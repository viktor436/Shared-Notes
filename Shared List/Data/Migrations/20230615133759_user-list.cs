using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_List.Data.Migrations
{
    public partial class userlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Lists",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_AspNetUsers_CreatorId",
                table: "Lists",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_AspNetUsers_CreatorId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CreatorId",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Lists");
        }
    }
}
