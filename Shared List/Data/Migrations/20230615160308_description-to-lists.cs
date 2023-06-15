using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared_List.Data.Migrations
{
    public partial class descriptiontolists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lists");
        }
    }
}
