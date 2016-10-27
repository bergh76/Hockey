using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigrationSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Maker__MakerCardManufactureId",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Maker",
                table: "Maker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardManufacture",
                table: "Maker",
                column: "CardManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_CardManufacture__MakerCardManufactureId",
                table: "Player",
                column: "_MakerCardManufactureId",
                principalTable: "Maker",
                principalColumn: "CardManufactureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "Maker",
                newName: "CardManufacture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_CardManufacture__MakerCardManufactureId",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardManufacture",
                table: "CardManufacture");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Maker",
                table: "CardManufacture",
                column: "CardManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Maker__MakerCardManufactureId",
                table: "Player",
                column: "_MakerCardManufactureId",
                principalTable: "CardManufacture",
                principalColumn: "CardManufactureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "CardManufacture",
                newName: "Maker");
        }
    }
}
