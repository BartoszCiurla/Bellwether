using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Bellwether.Repositories.Migrations
{
    public partial class languageVersioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LanguageVersion",
                table: "BellwetherLanguage",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "LanguageVersion", table: "BellwetherLanguage");
        }
    }
}
