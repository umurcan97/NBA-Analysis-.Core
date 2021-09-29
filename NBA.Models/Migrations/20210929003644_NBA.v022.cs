using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class NBAv022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OverTime",
                table: "FullSeason20_21",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OverTime",
                table: "FullSeason19_20",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OverTime",
                table: "FullSeason",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverTime",
                table: "FullSeason20_21");

            migrationBuilder.DropColumn(
                name: "OverTime",
                table: "FullSeason19_20");

            migrationBuilder.DropColumn(
                name: "OverTime",
                table: "FullSeason");
        }
    }
}
