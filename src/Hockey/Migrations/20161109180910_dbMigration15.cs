using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hockey.Migrations
{
    public partial class dbMigration15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PositionType",
                table: "Position",
                newName: "PositionShortName");

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Position",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Position");

            migrationBuilder.RenameColumn(
                name: "PositionShortName",
                table: "Position",
                newName: "PositionType");
        }
    }
}
