using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveScore.Data.Migrations
{
    public partial class AddingImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Player",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Player");
        }
    }
}
