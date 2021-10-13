using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class NBAv025 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerStatsQuarter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNo = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    PlayerPoints = table.Column<double>(type: "float", nullable: false),
                    PlayerAssists = table.Column<double>(type: "float", nullable: false),
                    PlayerDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    PlayerOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    PlayerBlocks = table.Column<double>(type: "float", nullable: false),
                    PlayerSteals = table.Column<double>(type: "float", nullable: false),
                    PlayerTurnovers = table.Column<double>(type: "float", nullable: false),
                    PlayerFGA = table.Column<double>(type: "float", nullable: false),
                    PlayerFGM = table.Column<double>(type: "float", nullable: false),
                    Player3PA = table.Column<double>(type: "float", nullable: false),
                    Player3PM = table.Column<double>(type: "float", nullable: false),
                    PlayerFTA = table.Column<double>(type: "float", nullable: false),
                    PlayerFTM = table.Column<double>(type: "float", nullable: false),
                    PlayerFouls = table.Column<double>(type: "float", nullable: false),
                    PlayerPlusMinus = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatsQuarter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatsQuarter_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatsQuarter19_20",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNo = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    PlayerPoints = table.Column<double>(type: "float", nullable: false),
                    PlayerAssists = table.Column<double>(type: "float", nullable: false),
                    PlayerDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    PlayerOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    PlayerBlocks = table.Column<double>(type: "float", nullable: false),
                    PlayerSteals = table.Column<double>(type: "float", nullable: false),
                    PlayerTurnovers = table.Column<double>(type: "float", nullable: false),
                    PlayerFGA = table.Column<double>(type: "float", nullable: false),
                    PlayerFGM = table.Column<double>(type: "float", nullable: false),
                    Player3PA = table.Column<double>(type: "float", nullable: false),
                    Player3PM = table.Column<double>(type: "float", nullable: false),
                    PlayerFTA = table.Column<double>(type: "float", nullable: false),
                    PlayerFTM = table.Column<double>(type: "float", nullable: false),
                    PlayerFouls = table.Column<double>(type: "float", nullable: false),
                    PlayerPlusMinus = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatsQuarter19_20", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatsQuarter19_20_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatsQuarter20_21",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNo = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<int>(type: "int", nullable: false),
                    PlayerPoints = table.Column<double>(type: "float", nullable: false),
                    PlayerAssists = table.Column<double>(type: "float", nullable: false),
                    PlayerDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    PlayerOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    PlayerBlocks = table.Column<double>(type: "float", nullable: false),
                    PlayerSteals = table.Column<double>(type: "float", nullable: false),
                    PlayerTurnovers = table.Column<double>(type: "float", nullable: false),
                    PlayerFGA = table.Column<double>(type: "float", nullable: false),
                    PlayerFGM = table.Column<double>(type: "float", nullable: false),
                    Player3PA = table.Column<double>(type: "float", nullable: false),
                    Player3PM = table.Column<double>(type: "float", nullable: false),
                    PlayerFTA = table.Column<double>(type: "float", nullable: false),
                    PlayerFTM = table.Column<double>(type: "float", nullable: false),
                    PlayerFouls = table.Column<double>(type: "float", nullable: false),
                    PlayerPlusMinus = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatsQuarter20_21", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatsQuarter20_21_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatsQuarter_PlayerId",
                table: "PlayerStatsQuarter",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatsQuarter19_20_PlayerId",
                table: "PlayerStatsQuarter19_20",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatsQuarter20_21_PlayerId",
                table: "PlayerStatsQuarter20_21",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerStatsQuarter");

            migrationBuilder.DropTable(
                name: "PlayerStatsQuarter19_20");

            migrationBuilder.DropTable(
                name: "PlayerStatsQuarter20_21");
        }
    }
}
