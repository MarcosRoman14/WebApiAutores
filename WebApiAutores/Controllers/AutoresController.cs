using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.DTOs;
using WebApiAutores.Entities;
using WebApiAutores.Filtros;

namespace WebApiAutores.Controllers
{
    [ApiController] // Validaciones automaticas respecto a la data recibida en el controlador
    [Route("api/autores")] // define la ruta del controlador (navegador)
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public AutoresController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }

        [HttpGet] // => ../api/autores/
        public async Task<ActionResult<List<AutorDTO>>> Get()
        {
            var autores =  await _context.Autores.ToListAsync();
            return mapper.Map<List<AutorDTO>>(autores);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTO>> Get(int id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);
           
            if (autor == null) return NotFound();

            return mapper.Map<AutorDTO>(autor);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<AutorDTO>>> Get(string nombre)
        {
            var autores = await _context.Autores.Where(x => x.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<AutorDTO>>(autores);
        }


        [HttpPost] // Agregar datos a la bd
        public async Task<ActionResult> Post([FromBody] AutorCreacionDTO autorCreacionDTO)
        {
            var existeAutorConElMismoNombre = await _context.Autores.AnyAsync(x => x.Nombre == autorCreacionDTO.Nombre);

            if (existeAutorConElMismoNombre)
                return BadRequest($"Ya existe un autor con el nombre {autorCreacionDTO.Nombre}");

            var autor = mapper.Map<Autor>(autorCreacionDTO);

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
