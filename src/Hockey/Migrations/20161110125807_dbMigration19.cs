using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigration19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_Image_ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_Team_TeamId",
                table: "NhlPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_TeamImage_TeamImageId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_TeamId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_TeamImageId",
                table: "NhlPlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_TeamId",
                table: "NhlPlayer",
                column: "TeamId");

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
                name: "FK_NhlPlayer_Team_TeamId",
                table: "NhlPlayer",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);

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
