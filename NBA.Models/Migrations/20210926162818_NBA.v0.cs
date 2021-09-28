using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class NBAv0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FullSeason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameNo = table.Column<int>(type: "int", nullable: false),
                    HomeTeam = table.Column<int>(type: "int", nullable: false),
                    AwayTeam = table.Column<int>(type: "int", nullable: false),
                    HomePoints = table.Column<double>(type: "float", nullable: false),
                    AwayPoints = table.Column<double>(type: "float", nullable: false),
                    HomeAssists = table.Column<double>(type: "float", nullable: false),
                    HomeDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    HomeOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    HomeBlocks = table.Column<double>(type: "float", nullable: false),
                    HomeSteals = table.Column<double>(type: "float", nullable: false),
                    HomeTurnovers = table.Column<double>(type: "float", nullable: false),
                    HomeFGA = table.Column<double>(type: "float", nullable: false),
                    HomeFGM = table.Column<double>(type: "float", nullable: false),
                    Home3PA = table.Column<double>(type: "float", nullable: false),
                    Home3PM = table.Column<double>(type: "float", nullable: false),
                    HomeFTA = table.Column<double>(type: "float", nullable: false),
                    HomeFTM = table.Column<double>(type: "float", nullable: false),
                    AwayAssists = table.Column<double>(type: "float", nullable: false),
                    AwayDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    AwayOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    AwayBlocks = table.Column<double>(type: "float", nullable: false),
                    AwaySteals = table.Column<double>(type: "float", nullable: false),
                    AwayTurnovers = table.Column<double>(type: "float", nullable: false),
                    AwayFGA = table.Column<double>(type: "float", nullable: false),
                    AwayFGM = table.Column<double>(type: "float", nullable: false),
                    Away3PA = table.Column<double>(type: "float", nullable: false),
                    Away3PM = table.Column<double>(type: "float", nullable: false),
                    AwayFTA = table.Column<double>(type: "float", nullable: false),
                    AwayFTM = table.Column<double>(type: "float", nullable: false),
                    HomeTotalRebounds = table.Column<double>(type: "float", nullable: false),
                    HomePITP = table.Column<double>(type: "float", nullable: false),
                    HomeFastBreakPoints = table.Column<double>(type: "float", nullable: false),
                    HomeBenchPoints = table.Column<double>(type: "float", nullable: false),
                    HomePointsofTO = table.Column<double>(type: "float", nullable: false),
                    AwayTotalRebounds = table.Column<double>(type: "float", nullable: false),
                    AwayPITP = table.Column<double>(type: "float", nullable: false),
                    AwayFastBreakPoints = table.Column<double>(type: "float", nullable: false),
                    AwayBenchPoints = table.Column<double>(type: "float", nullable: false),
                    AwayPointsofTO = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullSeason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullSeasonQuarters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNo = table.Column<int>(type: "int", nullable: false),
                    QuarterNo = table.Column<int>(type: "int", nullable: false),
                    HomeTeam = table.Column<int>(type: "int", nullable: false),
                    AwayTeam = table.Column<int>(type: "int", nullable: false),
                    HomePoints = table.Column<double>(type: "float", nullable: false),
                    AwayPoints = table.Column<double>(type: "float", nullable: false),
                    HomeAssists = table.Column<double>(type: "float", nullable: false),
                    HomeDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    HomeOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    HomeBlocks = table.Column<double>(type: "float", nullable: false),
                    HomeSteals = table.Column<double>(type: "float", nullable: false),
                    HomeTurnovers = table.Column<double>(type: "float", nullable: false),
                    HomeFGA = table.Column<double>(type: "float", nullable: false),
                    HomeFGM = table.Column<double>(type: "float", nullable: false),
                    Home3PA = table.Column<double>(type: "float", nullable: false),
                    Home3PM = table.Column<double>(type: "float", nullable: false),
                    HomeFTA = table.Column<double>(type: "float", nullable: false),
                    HomeFTM = table.Column<double>(type: "float", nullable: false),
                    AwayAssists = table.Column<double>(type: "float", nullable: false),
                    AwayDefensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    AwayOffensiveRebounds = table.Column<double>(type: "float", nullable: false),
                    AwayBlocks = table.Column<double>(type: "float", nullable: false),
                    AwaySteals = table.Column<double>(type: "float", nullable: false),
                    AwayTurnovers = table.Column<double>(type: "float", nullable: false),
                    AwayFGA = table.Column<double>(type: "float", nullable: false),
                    AwayFGM = table.Column<double>(type: "float", nullable: false),
                    Away3PA = table.Column<double>(type: "float", nullable: false),
                    Away3PM = table.Column<double>(type: "float", nullable: false),
                    AwayFTA = table.Column<double>(type: "float", nullable: false),
                    AwayFTM = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullSeasonQuarters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameNo = table.Column<int>(type: "int", nullable: false),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeTeam = table.Column<int>(type: "int", nullable: false),
                    AwayTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
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
                    table.PrimaryKey("PK_PlayerStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlayerStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStats_PlayerId",
                table: "PlayerStats",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullSeason");

            migrationBuilder.DropTable(
                name: "FullSeasonQuarters");

            migrationBuilder.DropTable(
                name: "GameTime");

            migrationBuilder.DropTable(
                name: "PlayerStats");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
