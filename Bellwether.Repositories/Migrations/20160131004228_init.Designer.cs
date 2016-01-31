using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bellwether.Repositories.Context;

namespace Bellwether.Repositories.Migrations
{
    [DbContext(typeof(BellwetherDbContext))]
    [Migration("20160131004228_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("Bellwether.Repositories.Entities.BellwetherLanguageDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LanguageName");

                    b.Property<string>("LanguageShortName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.GameFeatureDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameFeatureName");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.GameFeatureDetailDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameFeatureDetailName");

                    b.Property<int>("GameFeatureId");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.IntegrationGameDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IntegrationGameDescription");

                    b.Property<string>("IntegrationGameName");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.IntegrationGameFeatureDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameFeatureDetailId");

                    b.Property<int>("GameFeatureId");

                    b.Property<int>("IntegrationGameId");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.JokeCategoryDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("JokeCategoryName");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.JokeDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JokeCategoryId");

                    b.Property<string>("JokeContent");

                    b.Property<int>("LanguageId");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.GameFeatureDao", b =>
                {
                    b.HasOne("Bellwether.Repositories.Entities.BellwetherLanguageDao")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.GameFeatureDetailDao", b =>
                {
                    b.HasOne("Bellwether.Repositories.Entities.GameFeatureDao")
                        .WithMany()
                        .HasForeignKey("GameFeatureId");

                    b.HasOne("Bellwether.Repositories.Entities.BellwetherLanguageDao")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.IntegrationGameDao", b =>
                {
                    b.HasOne("Bellwether.Repositories.Entities.BellwetherLanguageDao")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.IntegrationGameFeatureDao", b =>
                {
                    b.HasOne("Bellwether.Repositories.Entities.GameFeatureDetailDao")
                        .WithMany()
                        .HasForeignKey("GameFeatureDetailId");

                    b.HasOne("Bellwether.Repositories.Entities.GameFeatureDao")
                        .WithMany()
                        .HasForeignKey("GameFeatureId");

                    b.HasOne("Bellwether.Repositories.Entities.IntegrationGameDao")
                        .WithMany()
                        .HasForeignKey("IntegrationGameId");

                    b.HasOne("Bellwether.Repositories.Entities.BellwetherLanguageDao")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.JokeCategoryDao", b =>
                {
                    b.HasOne("Bellwether.Repositories.Entities.BellwetherLanguageDao")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });

            modelBuilder.Entity("Bellwether.Repositories.Entities.JokeDao", b =>
                {
                    b.HasOne("Bellwether.Repositories.Entities.JokeCategoryDao")
                        .WithMany()
                        .HasForeignKey("JokeCategoryId");

                    b.HasOne("Bellwether.Repositories.Entities.BellwetherLanguageDao")
                        .WithMany()
                        .HasForeignKey("LanguageId");
                });
        }
    }
}
