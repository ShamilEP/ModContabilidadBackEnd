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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaContablesController : ControllerBase
    {
        private readonly sistema_contabilidadContext _context;

        public CuentaContablesController(sistema_contabilidadContext context)
        {
            _context = context;
        }

        // GET: api/CuentaContables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaContable>>> GetCuentaContable()
        {
            return await _context.CuentaContable.ToListAsync();
        }

        // GET: api/CuentaContables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CuentaContable>> GetCuentaContable(int id)
        {
            var cuentaContable = await _context.CuentaContable.FindAsync(id);

            if (cuentaContable == null)
            {
                return NotFound();
            }

            return cuentaContable;
        }

        // PUT: api/CuentaContables/5
        [ValidateModel]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaContable(int id, CuentaContable cuentaContable)
        {
            if (id != cuentaContable.Id)
            {
                return BadRequest();
            }

            _context.Entry(cuentaContable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaContableExists(id))
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

        // POST: api/CuentaContables
        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<CuentaContable>> PostCuentaContable(CuentaContable cuentaContable)
        {
            //int maxId = await _context.CuentaContable.MaxAsync(c => c.Id);
            //cuentaContable.Id = maxId;

            _context.CuentaContable.Add(cuentaContable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCuentaContable", new { id = cuentaContable.Id }, cuentaContable);
        }

        // DELETE: api/CuentaContables/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CuentaContable>> DeleteCuentaContable(int id)
        {
            var cuentaContable = await _context.CuentaContable.FindAsync(id);
            if (cuentaContable == null)
            {
                return NotFound();
            }

            _context.CuentaContable.Remove(cuentaContable);
            await _context.SaveChangesAsync();

            return cuentaContable;
        }

        private bool CuentaContableExists(int id)
        {
            return _context.CuentaContable.Any(e => e.Id == id);
        }
    }
}
