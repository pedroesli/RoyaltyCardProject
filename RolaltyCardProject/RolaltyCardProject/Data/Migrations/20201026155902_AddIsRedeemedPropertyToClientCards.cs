using Microsoft.EntityFrameworkCore.Migrations;

namespace RolaltyCardProject.Migrations
{
    public partial class AddIsRedeemedPropertyToClientCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRedeemed",
                table: "ClientCards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRedeemed",
                table: "ClientCards");
        }
    }
}
