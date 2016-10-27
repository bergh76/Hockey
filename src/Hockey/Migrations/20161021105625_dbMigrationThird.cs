using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigrationThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_CardManufacture__MakerCardManufactureId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player__MakerCardManufactureId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "MakerId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "_MakerCardManufactureId",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "CardManufactureId",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Player_CardManufactureId",
                table: "Player",
                column: "CardManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_CardManufacture_CardManufactureId",
                table: "Player",
                column: "CardManufactureId",
                principalTable: "CardManufacture",
                principalColumn: "CardManufactureId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_CardManufacture_CardManufactureId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_CardManufactureId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "CardManufactureId",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "MakerId",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "_MakerCardManufactureId",
                table: "Player",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player__MakerCardManufactureId",
                table: "Player",
                column: "_MakerCardManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_CardManufacture__MakerCardManufactureId",
                table: "Player",
                column: "_MakerCardManufactureId",
                principalTable: "CardManufacture",
                principalColumn: "CardManufactureId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
