using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigrationTenth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_League_LeagueId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_LeagueId",
                table: "NhlPlayer");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "NhlPlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "NhlPlayer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_LeagueId",
                table: "NhlPlayer",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhlPlayer_League_LeagueId",
                table: "NhlPlayer",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
