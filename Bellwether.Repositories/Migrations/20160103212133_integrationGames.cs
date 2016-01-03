using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bellwether.Repositories.Migrations
{
    public partial class integrationGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameFeatureDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameFeatureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFeatureDao", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "IntegrationGameDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IntegrationGameDescription = table.Column<string>(nullable: true),
                    IntegrationGameName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationGameDao", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "GameFeatureDetailDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameFeatureDaoId = table.Column<int>(nullable: true),
                    GameFeatureDetailName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFeatureDetailDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFeatureDetailDao_GameFeatureDao_GameFeatureDaoId",
                        column: x => x.GameFeatureDaoId,
                        principalTable: "GameFeatureDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "IntegrationGameFeatureDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameFeatureDetailId = table.Column<int>(nullable: true),
                    GameFeatureId = table.Column<int>(nullable: true),
                    IntegrationGameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationGameFeatureDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_GameFeatureDetailDao_GameFeatureDetailId",
                        column: x => x.GameFeatureDetailId,
                        principalTable: "GameFeatureDetailDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_GameFeatureDao_GameFeatureId",
                        column: x => x.GameFeatureId,
                        principalTable: "GameFeatureDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_IntegrationGameDao_IntegrationGameId",
                        column: x => x.IntegrationGameId,
                        principalTable: "IntegrationGameDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("IntegrationGameFeatureDao");
            migrationBuilder.DropTable("GameFeatureDetailDao");
            migrationBuilder.DropTable("IntegrationGameDao");
            migrationBuilder.DropTable("GameFeatureDao");
        }
    }
}
