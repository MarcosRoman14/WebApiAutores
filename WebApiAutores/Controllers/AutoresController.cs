using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;
using WebApiAutores.Filtros;

namespace WebApiAutores.Controllers
{
    [ApiController] // Validaciones automaticas respecto a la data recibida en el controlador
    [Route("api/autores")] // define la ruta del controlador (navegador)
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutoresController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet] // => ../api/autores/
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await _context.Autores./*Include(x => x.Libros).*/ToListAsync();
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            Autor autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null) return NotFound();

            return autor;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            Autor autor = await _context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null) return NotFound();

            return autor;
        }


        [HttpPost] // Agregar datos a la bd
        public async Task<ActionResult> Post(Autor autor)
        {
            var existeAutorConElMismoNombre = await _context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);
            if (existeAutorConElMismoNombre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }
            _context.Add(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] // Actualizar dato en la bd
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor.Id != id)
                return BadRequest("El id del autor no coincide con el id de la URL"); //Error 400 cometido por el cliente

            bool existe = await _context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            _context.Update(autor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")] // Eliminar dato de la bd
        public async Task<ActionResult> Delete(int id)
        {
            bool existe = await _context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
                return NotFound();

            _context.Remove(new Autor() { Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
