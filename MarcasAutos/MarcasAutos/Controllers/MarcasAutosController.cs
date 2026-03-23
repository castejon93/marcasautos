using MarcasAutos.Data;
using MarcasAutos.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcasAutosController : Controller
    {
        private readonly AppDbContext _context;

        public MarcasAutosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET api/marcasautos
        /// Returns all car brands from the database.
        /// </summary>
        /// <returns>List of MarcaAuto objects.</returns>
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetAll()
        {
            List<MarcaAuto> Brands = await _context.MarcasAutos.OrderBy(m => m.Name).ToListAsync();
            return Ok(Brands);
        }
    }
}
