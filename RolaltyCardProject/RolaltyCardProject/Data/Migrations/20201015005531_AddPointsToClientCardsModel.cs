using Microsoft.EntityFrameworkCore.Migrations;

namespace RolaltyCardProject.Migrations
{
    public partial class AddPointsToClientCardsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "ClientCards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "ClientCards");
        }
    }
}
