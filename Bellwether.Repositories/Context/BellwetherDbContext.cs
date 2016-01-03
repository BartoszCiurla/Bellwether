using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
