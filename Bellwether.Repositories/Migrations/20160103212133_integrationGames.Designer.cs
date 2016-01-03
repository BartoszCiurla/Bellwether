using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Bellwether.Repositories.Context;

namespace Bellwether.Repositories.Migrations
{
    [DbContext(typeof(BellwetherDbContext))]
    [Migration("20160103212133_integrationGames")]
    partial class integrationGames
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

            modelBuilder.Entity("Bellwether.Models.Entities.GameFeatureDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameFeatureName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.GameFeatureDetailDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GameFeatureDaoId");

                    b.Property<string>("GameFeatureDetailName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.IntegrationGameDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IntegrationGameDescription");

                    b.Property<string>("IntegrationGameName");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.IntegrationGameFeatureDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GameFeatureDetailId");

                    b.Property<int?>("GameFeatureId");

                    b.Property<int?>("IntegrationGameId");

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

            modelBuilder.Entity("Bellwether.Models.Entities.GameFeatureDetailDao", b =>
                {
                    b.HasOne("Bellwether.Models.Entities.GameFeatureDao")
                        .WithMany()
                        .HasForeignKey("GameFeatureDaoId");
                });

            modelBuilder.Entity("Bellwether.Models.Entities.IntegrationGameFeatureDao", b =>
                {
                    b.HasOne("Bellwether.Models.Entities.GameFeatureDetailDao")
                        .WithMany()
                        .HasForeignKey("GameFeatureDetailId");

                    b.HasOne("Bellwether.Models.Entities.GameFeatureDao")
                        .WithMany()
                        .HasForeignKey("GameFeatureId");

                    b.HasOne("Bellwether.Models.Entities.IntegrationGameDao")
                        .WithMany()
                        .HasForeignKey("IntegrationGameId");
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
