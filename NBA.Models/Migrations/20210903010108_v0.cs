using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class v0 : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullSeason");

            migrationBuilder.DropTable(
                name: "GameTime");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
