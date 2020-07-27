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
    public class MonedasController : ControllerBase
    {
        private readonly sistema_contabilidadContext _context;

        public MonedasController(sistema_contabilidadContext context)
        {
            _context = context;
        }

        // GET: api/Monedas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Moneda>>> GetMoneda()
        {
            return await _context.Moneda.ToListAsync();
        }

        // GET: api/Monedas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Moneda>> GetMoneda(int id)
        {
            var moneda = await _context.Moneda.FindAsync(id);

            if (moneda == null)
            {
                return NotFound();
            }

            return moneda;
        }

        // PUT: api/Monedas/5
        [ValidateModel]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoneda(int id, Moneda moneda)
        {
            if (id != moneda.Id)
            {
                return BadRequest();
            }

            _context.Entry(moneda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonedaExists(id))
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

        // POST: api/Monedas
        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<Moneda>> PostMoneda(Moneda moneda)
        {
           /* int maxId = await _context.Moneda.MaxAsync(m => m.Id);
            moneda.Id = maxId + 1;*/

            _context.Moneda.Add(moneda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneda", new { id = moneda.Id }, moneda);
        }

        // DELETE: api/Monedas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Moneda>> DeleteMoneda(int id)
        {
            var moneda = await _context.Moneda.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }

            _context.Moneda.Remove(moneda);
            await _context.SaveChangesAsync();

            return moneda;
        }

        private bool MonedaExists(int id)
        {
            return _context.Moneda.Any(e => e.Id == id);
        }
    }
}
