using MarcasAutos.Controllers;
using MarcasAutos.Data;
using MarcasAutos.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;

namespace MarcasAutos.Tests
{
    public class MarcasAutosControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly AppDbContext _contextInMemory;

        /// <summary>
        /// Initializes the test class with a test HTTP client from the factory.
        /// </summary>
        /// <param name="factory">The custom web application factory that sets up the test environment.</param>
        public MarcasAutosControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _contextInMemory = new Helpers().GetInMemoryContext();
        }

        /// <summary>
        /// Tests that the GetAll endpoint returns the expected data.
        /// Verifies:
        /// - HTTP 200 OK response
        /// - Correct number of records (3)
        /// - Each brand has the correct country and year
        /// </summary>
        [Fact]
        public async Task GetAll_FromDatabase()
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

        [Fact]
        public async Task GetAll_InMemory()
        {
            _contextInMemory.MarcasAutos.Add(new MarcaAuto { Id = 1, Country = "Italy", Name = "Ferrari", Year = 2020 });
            _contextInMemory.MarcasAutos.Add(new MarcaAuto { Id = 2, Country = "Germany", Name = "Mercedes Benz", Year = 2023 });
            await _contextInMemory.SaveChangesAsync();

            var cars = await _contextInMemory.MarcasAutos.ToListAsync();

            Assert.Equal(2, cars.Count);
        }

        [Fact]
        public async Task GetByBrand_InMemory()
        {
            _contextInMemory.MarcasAutos.Add(new MarcaAuto { Id = 1, Country = "Italy", Name = "Ferrari", Year = 2020 });
            _contextInMemory.MarcasAutos.Add(new MarcaAuto { Id = 2, Country = "Germany", Name = "Mercedes Benz", Year = 2023 });
            await _contextInMemory.SaveChangesAsync();

            var result = await _contextInMemory.MarcasAutos.Where(c => c.Name == "Ferrari").ToListAsync();

            Assert.All(result, c => Assert.Equal("Ferrari", c.Name));
        }

        [Fact]
        public async Task GetAllOkResult_InMemory()
        {
            // Arrange
            _contextInMemory.MarcasAutos.Add(new MarcaAuto { Id = 1, Country = "Italy", Name = "Ferrari", Year = 2020 });
            _contextInMemory.MarcasAutos.Add(new MarcaAuto { Id = 2, Country = "Germany", Name = "Mercedes Benz", Year = 2023 });
            await _contextInMemory.SaveChangesAsync();

            var controller = new MarcasAutosController(_contextInMemory);

            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}