using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class NBAv01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PlayerStats",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "FullSeason19_20",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_FullSeason19_20", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullSeason20_21",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_FullSeason20_21", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullSeasonQuarters19_20",
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
                    table.PrimaryKey("PK_FullSeasonQuarters19_20", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullSeasonQuarters20_21",
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
                    table.PrimaryKey("PK_FullSeasonQuarters20_21", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstGame = table.Column<bool>(type: "bit", nullable: false),
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
                    AwayFTM = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePredictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuarterPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstGame = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_QuarterPredictions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullSeason19_20");

            migrationBuilder.DropTable(
                name: "FullSeason20_21");

            migrationBuilder.DropTable(
                name: "FullSeasonQuarters19_20");

            migrationBuilder.DropTable(
                name: "FullSeasonQuarters20_21");

            migrationBuilder.DropTable(
                name: "GamePredictions");

            migrationBuilder.DropTable(
                name: "QuarterPredictions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PlayerStats",
                newName: "ID");
        }
    }
}
