using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModContabilidad.Filters;
using ModContabilidad.Models;

namespace ModContabilidad.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EntradaContablesController : ControllerBase
    {
        private readonly sistema_contabilidadContext _context;

        public EntradaContablesController(sistema_contabilidadContext context)
        {
            _context = context;
        }

        // GET: api/EntradaContables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntradaContable>>> GetEntradaContable()
        {
            return await _context.EntradaContable.Include(d => d.DetalleEntradaContable).ToListAsync();
        }

        // GET: api/EntradaContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntradaContable>> GetEntradaContable(int id)
        {
            var entradaContable = await _context.EntradaContable.FindAsync(id);

            if (entradaContable == null)
            {
                return NotFound();
            }

            return entradaContable;
        }

        // PUT: api/EntradaContables/5
        [ValidateModel]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradaContable(int id, EntradaContable entradaContable)
        {
            if (id != entradaContable.Id)
            {
                return BadRequest();
            }

            _context.Entry(entradaContable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradaContableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EntradaContables
        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<EntradaContable>> PostEntradaContable(EntradaContable entradaContable)
        {
            //fecha automatica by ignacio
            entradaContable.Fecha = DateTime.Now;

            //moneda automatica
            entradaContable.MonedaId = (entradaContable.MonedaId > 0) ? entradaContable.MonedaId : 1;
            var moneda = await _context.Moneda.FindAsync(entradaContable.MonedaId);

            //monto automatica by ignacio
            entradaContable.Monto = (entradaContable.Monto > 0) ? 
                entradaContable.Monto : 
                entradaContable.DetalleEntradaContable.Select(d => d.Monto).Sum() * moneda.Tasa;

            _context.EntradaContable.Add(entradaContable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntradaContable", new { id = entradaContable.Id }, entradaContable);
        }

        // DELETE: api/EntradaContables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EntradaContable>> DeleteEntradaContable(int id)
        {
            var entradaContable = await _context.EntradaContable.FindAsync(id);
            if (entradaContable == null)
            {
                return NotFound();
            }

            _context.EntradaContable.Remove(entradaContable);
            await _context.SaveChangesAsync();

            return entradaContable;
        }

        private bool EntradaContableExists(int id)
        {
            return _context.EntradaContable.Any(e => e.Id == id);
        }
    }
}
