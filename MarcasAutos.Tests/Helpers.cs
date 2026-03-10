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
    }
}
