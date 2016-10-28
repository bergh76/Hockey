using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigration11th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_TeamImage_TeamImageId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_TeamImageId",
                table: "NhlPlayer");

            migrationBuilder.DropColumn(
                name: "TeamImageId",
                table: "NhlPlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamImageId",
                table: "NhlPlayer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_TeamImageId",
                table: "NhlPlayer",
                column: "TeamImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhlPlayer_TeamImage_TeamImageId",
                table: "NhlPlayer",
                column: "TeamImageId",
                principalTable: "TeamImage",
                principalColumn: "TeamImageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
