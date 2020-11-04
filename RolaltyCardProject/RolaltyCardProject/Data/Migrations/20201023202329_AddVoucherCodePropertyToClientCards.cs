using Microsoft.EntityFrameworkCore.Migrations;

namespace RolaltyCardProject.Migrations
{
    public partial class AddVoucherCodePropertyToClientCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "ClientCards",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "ClientCards");
        }
    }
}
