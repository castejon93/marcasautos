using MarcasAutos.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MarcaAuto> MarcasAutos => Set<MarcaAuto>();

        /// <summary>
        /// Configures entity models and relationships when the context is created.
        /// Sets up the model conventions and initial data seeding.
        /// </summary>
        /// <param name="modelBuilder">Provides a simple API for configuring the EF model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply base configurations
            base.OnModelCreating(modelBuilder);

            // Seed initial car brand data into the MarcasAutos table
            // This data will be inserted during database migrations
            modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto
                {
                    Id = 1,
                    Name = "Toyota",
                    Country = "Japón",
                    Year = 1937
                },
                new MarcaAuto
                {
                    Id = 2,
                    Name = "Ford",
                    Country = "Estados Unidos",
                    Year = 1903
                },
                new MarcaAuto
                {
                    Id = 3,
                    Name = "BMW",
                    Country = "Alemania",
                    Year = 1916
                }
            );
        }
    }
}
