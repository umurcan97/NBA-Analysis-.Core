using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBA.Models.Migrations
{
    public partial class NBAv027 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameTime18_19",
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
                    table.PrimaryKey("PK_GameTime18_19", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTime18_19");
        }
    }
}
