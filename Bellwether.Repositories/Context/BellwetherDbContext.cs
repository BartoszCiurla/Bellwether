using System;
using System.IO;
using Windows.Storage;
using Bellwether.Models.Entities;
using Microsoft.Data.Entity;

namespace Bellwether.Repositories.Context
{
    public class BellwetherDbContext:DbContext
    {
        public DbSet<BellwetherLanguageDao> BellwetherLanguages { get; set; }
        public DbSet<JokeDao> Jokes { get; set; }
        public DbSet<JokeCategoryDao> JokeCategories { get; set; }
        public DbSet<GameFeatureDao> GameFeatures { get; set; }
        public DbSet<GameFeatureDetailDao> GameFeatureDetails { get; set; }
        public DbSet<IntegrationGameFeatureDao> IntegrationGameFeatures { get; set; }
        public DbSet<IntegrationGameDao> IntegrationGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "Testy4.db";
            try
            {
                databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            }
            catch (InvalidOperationException)
            {

            }
            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BellwetherLanguageDao>();
            modelBuilder.Entity<JokeDao>();
            modelBuilder.Entity<JokeCategoryDao>();
            modelBuilder.Entity<GameFeatureDao>();
            modelBuilder.Entity<GameFeatureDetailDao>();
            modelBuilder.Entity<IntegrationGameFeatureDao>();
            modelBuilder.Entity<IntegrationGameDao>();
        }
    }
}
