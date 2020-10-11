using Microsoft.EntityFrameworkCore.Migrations;

namespace RolaltyCardProject.Data.Migrations
{
    public partial class AddClientUsersAndClientCards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientCards",
                columns: table => new
                {
                    LoyaltyCardId = table.Column<int>(nullable: false),
                    ClientUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCards", x => new { x.ClientUserId, x.LoyaltyCardId });
                    table.ForeignKey(
                        name: "FK_ClientCards_AspNetUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientCards_LoyaltyCards_LoyaltyCardId",
                        column: x => x.LoyaltyCardId,
                        principalTable: "LoyaltyCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCards_LoyaltyCardId",
                table: "ClientCards",
                column: "LoyaltyCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientCards");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "AspNetUsers");
        }
    }
}
