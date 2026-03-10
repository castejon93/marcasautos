using MarcasAutos.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Testcontainers.PostgreSql;
using Xunit;

namespace MarcasAutos.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>, IAsyncLifetime
        where TStartup : class
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// PostgreSQL test container configured with credentials from appsettings.json.
        /// Docker must be running for this to work.
        /// </summary>
        private PostgreSqlContainer _postgresContainer;

        /// <summary>
        /// Initializes the factory with configuration from appsettings.json.
        /// </summary>
        public CustomWebApplicationFactory()
        {
            // Load configuration from appsettings.json using Helper
            _configuration = Helpers.LoadConfiguration();
        }

        /// <summary>
        /// Starts the PostgreSQL container before running any tests.
        /// Called automatically by xUnit before test execution.
        /// Reads database name, username, and password from appsettings.json.
        /// </summary>
        public async Task InitializeAsync()
        {
            // Get the connection string from configuration
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            // Use NpgsqlConnectionStringBuilder to properly parse the connection string
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(connectionString);

            // Create and start the PostgreSQL container with credentials from appsettings.json
            _postgresContainer = new PostgreSqlBuilder()
                .WithDatabase(connectionStringBuilder.Database)
                .WithUsername(connectionStringBuilder.Username)
                .WithPassword(connectionStringBuilder.Password)
                .Build();

            await _postgresContainer.StartAsync();
        }

        /// <summary>
        /// Stops and disposes the PostgreSQL container after all tests complete.
        /// Called automatically by xUnit after test execution.
        /// </summary>
        public new async Task DisposeAsync()
        {
            if (_postgresContainer != null)
            {
                await _postgresContainer.StopAsync();
                await _postgresContainer.DisposeAsync();
            }
        }

        /// <summary>
        /// Configures the web host to use the test PostgreSQL container instead of the production database.
        /// Also runs migrations and seeds test data.
        /// </summary>
        /// <param name="builder">The web host builder to configure.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the default DbContext configuration that points to production database
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));

                // Register AppDbContext to use the test PostgreSQL container
                services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(_postgresContainer.GetConnectionString()));
            });
        }
    }
}
