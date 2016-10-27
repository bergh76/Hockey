using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigrationFourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Image_ImageId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_ImageId",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "_ImageImageId",
                table: "Player",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player__ImageImageId",
                table: "Player",
                column: "_ImageImageId");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Image",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Image__ImageImageId",
                table: "Player",
                column: "_ImageImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Image__ImageImageId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player__ImageImageId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "_ImageImageId",
                table: "Player");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ImageId",
                table: "Player",
                column: "ImageId");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerId",
                table: "Image",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Image_ImageId",
                table: "Player",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
