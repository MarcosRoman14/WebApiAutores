﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entities;

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

        //Comunicaciónes a base de datos una buena practica es usar programación async
        [HttpGet] // Obtener datos de la bd
        public async Task<ActionResult<List<Autor>>> Get() 
        {
            return await _context.Autores.ToListAsync();
        }

        [HttpPost] // Agregar datos a la bd
        public async Task<ActionResult> Post(Autor autor)
        {
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
            bool existe = await _context.Autores.AnyAsync(x=> x.Id== id);

            if(!existe)
                return NotFound();

            _context.Remove(new Autor() {Id = id });
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}
