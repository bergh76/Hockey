using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hockey.Migrations
{
    public partial class dbMigrationSixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_NhlTeam_NhlTeamId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_NhlTeamId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "TeamImage");

            migrationBuilder.DropColumn(
                name: "NhlTeamId",
                table: "TeamImage");

            migrationBuilder.DropColumn(
                name: "NhlTeamId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "NhlTeam");

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "NhlPlayer",
                columns: table => new
                {
                    NhlPlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardManufactureId = table.Column<int>(nullable: false),
                    ConferenceId = table.Column<int>(nullable: false),
                    DivisionId = table.Column<int>(nullable: false),
                    ISActive = table.Column<bool>(nullable: false),
                    ISSigned = table.Column<bool>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    NationalityId = table.Column<int>(nullable: false),
                    NhlPlayerCardId = table.Column<string>(nullable: true),
                    PlayerAddDate = table.Column<DateTime>(nullable: false),
                    PlayerFirstName = table.Column<string>(nullable: true),
                    PlayerImage = table.Column<string>(nullable: true),
                    PlayerJersyNumber = table.Column<int>(nullable: false),
                    PlayerLastName = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TeamImageId = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhlPlayer", x => x.NhlPlayerId);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_CardManufacture_CardManufactureId",
                        column: x => x.CardManufactureId,
                        principalTable: "CardManufacture",
                        principalColumn: "CardManufactureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Conference_ConferenceId",
                        column: x => x.ConferenceId,
                        principalTable: "Conference",
                        principalColumn: "ConferenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "Division",
                        principalColumn: "DivisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Nationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationality",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhlPlayer_TeamImage_TeamImageId",
                        column: x => x.TeamImageId,
                        principalTable: "TeamImage",
                        principalColumn: "TeamImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShlPlayer",
                columns: table => new
                {
                    ShlPlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardManufactureId = table.Column<int>(nullable: false),
                    ISActive = table.Column<bool>(nullable: false),
                    ISSigned = table.Column<bool>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    LeagueId = table.Column<int>(nullable: false),
                    NationalityId = table.Column<int>(nullable: false),
                    PlayerAddDate = table.Column<DateTime>(nullable: false),
                    PlayerCardId = table.Column<string>(nullable: true),
                    PlayerFirstName = table.Column<string>(nullable: true),
                    PlayerImage = table.Column<string>(nullable: true),
                    PlayerJersyNumber = table.Column<int>(nullable: false),
                    PlayerLastName = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    SeasonId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    TeamImageId = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShlPlayer", x => x.ShlPlayerId);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_CardManufacture_CardManufactureId",
                        column: x => x.CardManufactureId,
                        principalTable: "CardManufacture",
                        principalColumn: "CardManufactureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_Image_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Image",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_League_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "League",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_Nationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationality",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Position",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "SeasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShlPlayer_TeamImage_TeamImageId",
                        column: x => x.TeamImageId,
                        principalTable: "TeamImage",
                        principalColumn: "TeamImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Player_TeamId",
                table: "Player",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_CardManufactureId",
                table: "NhlPlayer",
                column: "CardManufactureId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_ConferenceId",
                table: "NhlPlayer",
                column: "ConferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_DivisionId",
                table: "NhlPlayer",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_ImageId",
                table: "NhlPlayer",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_LeagueId",
                table: "NhlPlayer",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_NationalityId",
                table: "NhlPlayer",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_PositionId",
                table: "NhlPlayer",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_SeasonId",
                table: "NhlPlayer",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_TeamId",
                table: "NhlPlayer",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_NhlPlayer_TeamImageId",
                table: "NhlPlayer",
                column: "TeamImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_CardManufactureId",
                table: "ShlPlayer",
                column: "CardManufactureId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_ImageId",
                table: "ShlPlayer",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_LeagueId",
                table: "ShlPlayer",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_NationalityId",
                table: "ShlPlayer",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_PositionId",
                table: "ShlPlayer",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_SeasonId",
                table: "ShlPlayer",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_TeamId",
                table: "ShlPlayer",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ShlPlayer_TeamImageId",
                table: "ShlPlayer",
                column: "TeamImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Team_TeamId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_TeamId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Player");

            migrationBuilder.DropTable(
                name: "NhlPlayer");

            migrationBuilder.DropTable(
                name: "ShlPlayer");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.CreateTable(
                name: "NhlTeam",
                columns: table => new
                {
                    NhlTeamId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConferenceId = table.Column<int>(nullable: false),
                    DivisionId = table.Column<int>(nullable: false),
                    NhlTeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhlTeam", x => x.NhlTeamId);
                });

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "TeamImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NhlTeamId",
                table: "TeamImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NhlTeamId",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Player_NhlTeamId",
                table: "Player",
                column: "NhlTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_NhlTeam_NhlTeamId",
                table: "Player",
                column: "NhlTeamId",
                principalTable: "NhlTeam",
                principalColumn: "NhlTeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
