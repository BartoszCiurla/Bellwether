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
                name: "JokeCategoryDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JokeCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JokeCategoryDao", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "JokeDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JokeCategoryId = table.Column<int>(nullable: true),
                    JokeContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JokeDao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JokeDao_JokeCategoryDao_JokeCategoryId",
                        column: x => x.JokeCategoryId,
                        principalTable: "JokeCategoryDao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BellwetherLanguageDao");
            migrationBuilder.DropTable("JokeDao");
            migrationBuilder.DropTable("JokeCategoryDao");
        }
    }
}
