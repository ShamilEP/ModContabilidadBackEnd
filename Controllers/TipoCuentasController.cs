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
    public class TipoCuentasController : ControllerBase
    {
        private readonly sistema_contabilidadContext _context;

        public TipoCuentasController(sistema_contabilidadContext context)
        {
            _context = context;
        }

        // GET: api/TipoCuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCuenta>>> GetTipoCuenta()
        {
            return await _context.TipoCuenta.ToListAsync();
        }

        // GET: api/TipoCuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoCuenta>> GetTipoCuenta(int id)
        {
            var tipoCuenta = await _context.TipoCuenta.FindAsync(id);

            if (tipoCuenta == null)
            {
                return NotFound();
            }

            return tipoCuenta;
        }

        // PUT: api/TipoCuentas/5
        [ValidateModel]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoCuenta(int id, TipoCuenta tipoCuenta)
        {
            if (id != tipoCuenta.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoCuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoCuentaExists(id))
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

        // POST: api/TipoCuentas
        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<TipoCuenta>> PostTipoCuenta(TipoCuenta tipoCuenta)
        {
            int maxId = await _context.TipoCuenta.MaxAsync(t => t.Id);
            tipoCuenta.Id = maxId + 1;

            _context.TipoCuenta.Add(tipoCuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoCuenta", new { id = tipoCuenta.Id }, tipoCuenta);
        }

        // DELETE: api/TipoCuentas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TipoCuenta>> DeleteTipoCuenta(int id)
        {
            var tipoCuenta = await _context.TipoCuenta.FindAsync(id);
            if (tipoCuenta == null)
            {
                return NotFound();
            }

            _context.TipoCuenta.Remove(tipoCuenta);
            await _context.SaveChangesAsync();

            return tipoCuenta;
        }

        private bool TipoCuentaExists(int id)
        {
            return _context.TipoCuenta.Any(e => e.Id == id);
        }
    }
}
