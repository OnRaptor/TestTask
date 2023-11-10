using Microsoft.EntityFrameworkCore;
using TestTask.Data.Models;

namespace TestTask.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().ToTable("Brand");
            modelBuilder.Entity<Model>().ToTable("Model");
        }
    }
}
