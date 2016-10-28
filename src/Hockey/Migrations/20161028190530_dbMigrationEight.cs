using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigrationEight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NhlPlayer_Image_ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "NhlPlayer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "NhlPlayer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_NhlPlayer_Image_ImageId",
                table: "NhlPlayer",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
