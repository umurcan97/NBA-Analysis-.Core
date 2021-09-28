using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NBA.Models.Migrations
{
    public partial class NBAv02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameDate",
                table: "FullSeason");

            migrationBuilder.AddColumn<int>(
                name: "GameDateId",
                table: "FullSeason20_21",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameDateId",
                table: "FullSeason19_20",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameDateId",
                table: "FullSeason",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameTime19_20",
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
                    table.PrimaryKey("PK_GameTime19_20", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTime20_21",
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
                    table.PrimaryKey("PK_GameTime20_21", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FullSeason20_21_GameDateId",
                table: "FullSeason20_21",
                column: "GameDateId");

            migrationBuilder.CreateIndex(
                name: "IX_FullSeason19_20_GameDateId",
                table: "FullSeason19_20",
                column: "GameDateId");

            migrationBuilder.CreateIndex(
                name: "IX_FullSeason_GameDateId",
                table: "FullSeason",
                column: "GameDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FullSeason_GameTime_GameDateId",
                table: "FullSeason",
                column: "GameDateId",
                principalTable: "GameTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FullSeason19_20_GameTime19_20_GameDateId",
                table: "FullSeason19_20",
                column: "GameDateId",
                principalTable: "GameTime19_20",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FullSeason20_21_GameTime20_21_GameDateId",
                table: "FullSeason20_21",
                column: "GameDateId",
                principalTable: "GameTime20_21",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FullSeason_GameTime_GameDateId",
                table: "FullSeason");

            migrationBuilder.DropForeignKey(
                name: "FK_FullSeason19_20_GameTime19_20_GameDateId",
                table: "FullSeason19_20");

            migrationBuilder.DropForeignKey(
                name: "FK_FullSeason20_21_GameTime20_21_GameDateId",
                table: "FullSeason20_21");

            migrationBuilder.DropTable(
                name: "GameTime19_20");

            migrationBuilder.DropTable(
                name: "GameTime20_21");

            migrationBuilder.DropIndex(
                name: "IX_FullSeason20_21_GameDateId",
                table: "FullSeason20_21");

            migrationBuilder.DropIndex(
                name: "IX_FullSeason19_20_GameDateId",
                table: "FullSeason19_20");

            migrationBuilder.DropIndex(
                name: "IX_FullSeason_GameDateId",
                table: "FullSeason");

            migrationBuilder.DropColumn(
                name: "GameDateId",
                table: "FullSeason20_21");

            migrationBuilder.DropColumn(
                name: "GameDateId",
                table: "FullSeason19_20");

            migrationBuilder.DropColumn(
                name: "GameDateId",
                table: "FullSeason");

            migrationBuilder.AddColumn<DateTime>(
                name: "GameDate",
                table: "FullSeason",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
