using MarcasAutos.Data;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.Migrations
{
    public static class MigrationExtensions
    {
        /// <summary>
        /// Applies all pending migrations to the database.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void ApplyMigration(this IApplicationBuilder app)
        {
            // Create a service scope to resolve dependencies
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            // Get the AppDbContext and apply migrations
            using AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();
        }
    }
}
