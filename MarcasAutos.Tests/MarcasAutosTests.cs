using MarcasAutos.Models;
using System.Net;
using System.Net.Http.Json;

namespace MarcasAutos.Tests
{
    public class MarcasAutosControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes the test class with a test HTTP client from the factory.
        /// </summary>
        /// <param name="factory">The custom web application factory that sets up the test environment.</param>
        public MarcasAutosControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Tests that the GetAll endpoint returns the expected data.
        /// Verifies:
        /// - HTTP 200 OK response
        /// - Correct number of records (3)
        /// - Each brand has the correct country and year
        /// </summary>
        [Fact]
        public async Task GetAll_ReturnsExpectedData()
        {
            // Act: Call the GET /MarcasAutos endpoint
            var response = await _client.GetAsync("/MarcasAutos");

            // Assert: Verify HTTP 200 OK status
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Deserialize the JSON response into a list of MarcaAuto objects
            var marcas = await response.Content.ReadFromJsonAsync<List<MarcaAuto>>();

            // Assert: Response contains data and has exactly 3 records
            Assert.NotNull(marcas);
            Assert.Equal(3, marcas!.Count);

            // Assert: Verify each seeded brand has the correct country
            Assert.Equal("Japón", marcas.First(m => m.Name == "Toyota").Country);
            Assert.Equal("Estados Unidos", marcas.First(m => m.Name == "Ford").Country);
            Assert.Equal("Alemania", marcas.First(m => m.Name == "BMW").Country);
        }
    }
}