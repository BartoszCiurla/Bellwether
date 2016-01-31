using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bellwether.Repositories.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BellwetherLanguageDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LanguageName = table.Column<string>(nullable: true),
                    LanguageShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BellwetherLanguageDao", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "GameFeatureDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameFeatureName = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFeatureDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFeatureDao_BellwetherLanguageDao_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "BellwetherLanguageDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "IntegrationGameDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IntegrationGameDescription = table.Column<string>(nullable: true),
                    IntegrationGameName = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationGameDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationGameDao_BellwetherLanguageDao_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "BellwetherLanguageDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "JokeCategoryDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JokeCategoryName = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JokeCategoryDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JokeCategoryDao_BellwetherLanguageDao_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "BellwetherLanguageDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "GameFeatureDetailDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameFeatureDetailName = table.Column<string>(nullable: true),
                    GameFeatureId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFeatureDetailDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameFeatureDetailDao_GameFeatureDao_GameFeatureId",
                        column: x => x.GameFeatureId,
                        principalTable: "GameFeatureDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameFeatureDetailDao_BellwetherLanguageDao_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "BellwetherLanguageDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "JokeDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JokeCategoryId = table.Column<int>(nullable: false),
                    JokeContent = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JokeDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JokeDao_JokeCategoryDao_JokeCategoryId",
                        column: x => x.JokeCategoryId,
                        principalTable: "JokeCategoryDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JokeDao_BellwetherLanguageDao_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "BellwetherLanguageDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "IntegrationGameFeatureDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GameFeatureDetailId = table.Column<int>(nullable: false),
                    GameFeatureId = table.Column<int>(nullable: false),
                    IntegrationGameId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationGameFeatureDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_GameFeatureDetailDao_GameFeatureDetailId",
                        column: x => x.GameFeatureDetailId,
                        principalTable: "GameFeatureDetailDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_GameFeatureDao_GameFeatureId",
                        column: x => x.GameFeatureId,
                        principalTable: "GameFeatureDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_IntegrationGameDao_IntegrationGameId",
                        column: x => x.IntegrationGameId,
                        principalTable: "IntegrationGameDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntegrationGameFeatureDao_BellwetherLanguageDao_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "BellwetherLanguageDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("IntegrationGameFeatureDao");
            migrationBuilder.DropTable("JokeDao");
            migrationBuilder.DropTable("GameFeatureDetailDao");
            migrationBuilder.DropTable("IntegrationGameDao");
            migrationBuilder.DropTable("JokeCategoryDao");
            migrationBuilder.DropTable("GameFeatureDao");
            migrationBuilder.DropTable("BellwetherLanguageDao");
        }
    }
}
