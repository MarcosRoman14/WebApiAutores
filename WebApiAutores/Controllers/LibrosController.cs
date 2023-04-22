using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LibrosController(ApplicationDbContext context)
        {
            this._context = context;
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Libro>> Get(int id)
        //{
        //    return await _context.Libros.Include(x=> x.Autor).FirstOrDefaultAsync(x=> x.Id == id);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Post(Libro libro)
        //{
        //    bool existeAutor = await _context.Autores.AnyAsync(x => x.Id == libro.AutorId);

        //    if (!existeAutor) return BadRequest($"No existe el autor de Id: {libro.AutorId}");

        //    _context.Add(libro);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
