using Microsoft.EntityFrameworkCore;
using MoscowWeatherAPI.Models;

namespace MoscowWeatherAPI
{
    public class DbMainContext : DbContext
    {
        public DbMainContext(DbContextOptions<DbMainContext> options) : base(options)
        {
            Database.AutoTransactionsEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherData>();
        }
    }
}
