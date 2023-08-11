using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveScore.Data.Migrations
{
    public partial class SecondDatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoosingTeamId",
                table: "Match",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Match_LoosingTeamId",
                table: "Match",
                column: "LoosingTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Team_LoosingTeamId",
                table: "Match",
                column: "LoosingTeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_Team_LoosingTeamId",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_LoosingTeamId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "LoosingTeamId",
                table: "Match");
        }
    }
}
