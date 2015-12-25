using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bellwether.Repositories.Context;

namespace Bellwether.Repositories.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20151224133143_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Bellwether.Models.Models.BellwetherLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName");

                    b.Property<string>("LanguageShortName");

                    b.HasKey("Id");
                });
        }
    }
}
