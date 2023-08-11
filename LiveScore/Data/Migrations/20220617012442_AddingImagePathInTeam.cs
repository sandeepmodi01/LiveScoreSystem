using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveScore.Data.Migrations
{
    public partial class AddingImagePathInTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Team",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Team");
        }
    }
}
