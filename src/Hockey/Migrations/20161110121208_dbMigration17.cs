using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigration17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "NhlPlayer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "NhlPlayer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeamImageId",
                table: "NhlPlayer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_LeagueId",
                table: "NhlPlayer",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_TeamImageId",
                table: "NhlPlayer",
                column: "TeamImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhlPlayer_Image_ImageId",
                table: "NhlPlayer",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhlPlayer_League_LeagueId",
                table: "NhlPlayer",
                column: "LeagueId",
                principalTable: "League",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NhlPlayer_TeamImage_TeamImageId",
                table: "NhlPlayer",
                column: "TeamImageId",
                principalTable: "TeamImage",
                principalColumn: "TeamImageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_Image_ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_League_LeagueId",
                table: "NhlPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_TeamImage_TeamImageId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_LeagueId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_TeamImageId",
                table: "NhlPlayer");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "NhlPlayer");

            migrationBuilder.DropColumn(
                name: "TeamImageId",
                table: "NhlPlayer");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
