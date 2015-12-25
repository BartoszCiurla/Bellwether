using System;
using System.IO;
using Windows.Storage;
using Bellwether.Models.Models;
using Microsoft.Data.Entity;

namespace Bellwether.Repositories.Context
{
    public class DataContext:DbContext
    {
        public DbSet<BellwetherLanguage> BellwetherLanguages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "Bellwether.db";
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
            modelBuilder.Entity<BellwetherLanguage>();

        }
    }
}
