using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bellwether.Repositories.Context;

namespace Bellwether.Repositories.Migrations
{
    [DbContext(typeof(BellwetherDbContext))]
    [Migration("20160103165504_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Bellwether.Models.Entities.BellwetherLanguageDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName");

                    b.Property<string>("LanguageShortName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.JokeCategoryDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JokeCategoryName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.JokeDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("JokeCategoryId");

                    b.Property<string>("JokeContent");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.JokeDao", b =>
                {
                    b.HasOne("Bellwether.Models.Entities.JokeCategoryDao")
                        .WithMany()
                        .HasForeignKey("JokeCategoryId");
                });
        }
    }
}
