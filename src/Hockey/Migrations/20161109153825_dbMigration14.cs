using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigration14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Image__ImageImageId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.RenameColumn(
                name: "_ImageImageId",
                table: "Player",
                newName: "ImageId1");

            migrationBuilder.RenameIndex(
                name: "IX_Player__ImageImageId",
                table: "Player",
                newName: "IX_Player_ImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Image_ImageId1",
                table: "Player",
                column: "ImageId1",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Image_ImageId1",
                table: "Player");

            migrationBuilder.RenameColumn(
                name: "ImageId1",
                table: "Player",
                newName: "_ImageImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_ImageId1",
                table: "Player",
                newName: "IX_Player__ImageImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Image__ImageImageId",
                table: "Player",
                column: "_ImageImageId",
                principalTable: "Image",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
