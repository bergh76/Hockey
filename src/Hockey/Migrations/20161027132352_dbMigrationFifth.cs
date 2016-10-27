using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hockey.Migrations
{
    public partial class dbMigrationFifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "League",
                columns: table => new
                {
                    LeagueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeagueName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_League", x => x.LeagueId);
                });

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "TeamImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NhlTeamId",
                table: "TeamImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Player_LeagueId",
                table: "Player",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_League_LeagueId",
                table: "Player",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_League_LeagueId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_LeagueId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "TeamImage");

            migrationBuilder.DropColumn(
                name: "NhlTeamId",
                table: "TeamImage");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "League");
        }
    }
}
