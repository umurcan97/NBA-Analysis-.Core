using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class NBAv026 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuarterNo",
                table: "PlayerStatsQuarter20_21",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuarterNo",
                table: "PlayerStatsQuarter19_20",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuarterNo",
                table: "PlayerStatsQuarter20_21");

            migrationBuilder.DropColumn(
                name: "QuarterNo",
                table: "PlayerStatsQuarter19_20");
        }
    }
}
