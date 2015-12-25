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
                name: "BellwetherLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    LanguageName = table.Column<string>(nullable: true),
                    LanguageShortName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BellwetherLanguage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BellwetherLanguage");
        }
    }
}
