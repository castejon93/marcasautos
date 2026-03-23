using MarcasAutos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MarcasAutos.Tests
{
    internal class Helpers
    {
        /// <summary>
        /// Loads the configuration from the test project's appsettings.json file.
        /// </summary>
        /// <returns>An IConfiguration instance with the loaded configuration.</returns>
        public static IConfiguration LoadConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return configBuilder.Build();
        }

        public AppDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            return new AppDbContext(options); 
        }
    }
}
