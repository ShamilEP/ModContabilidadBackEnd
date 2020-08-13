using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModContabilidad.Filters;
using ModContabilidad.Models;

namespace ModContabilidad.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet("dummy")]
        public ActionResult<EntradaContable> GetDummyEntradaContable()
        {
            var obj = new EntradaContable
            {
                Id = 0,
                AuxiliarId = 0,
                Descripcion = "Entrada Contable para Junio 2020",
                Estado = true,
                Fecha = new DateTime(1, 1, 1),
                MonedaId = 0,
                Monto = 10000.00,
                DetalleEntradaContable = new List<DetalleEntradaContable>
                {
                    new DetalleEntradaContable { Id = 0, CuentaContableId = 0, Descripcion = "Breve descripción del detalle 1.", EntradaContableId = 0, TipoMovimiento = "CR", Monto = 5000.00, Estado = true },
                    new DetalleEntradaContable { Id = 0, CuentaContableId = 0, Descripcion = "Breve descripción del detalle 2.", EntradaContableId = 0, TipoMovimiento = "DB", Monto = 5000.00, Estado = true }
                }
            };

            return obj;
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
        [AllowAnonymous]
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
        /// <summary>
        /// Método para registrar una entrada contable con sus detalles.
        /// Para el correcto funcionamietno de este método es necesario enviar un objeto EntradaContable con por lo menos dos detalle, uno en crédito y otro en débito.
        /// </summary>
        /// <param name="entradaContable"></param>
        /// <returns>Retorna el objeto EntradaContable con su Id generado en base de datos.</returns>
        [AllowAnonymous]
        [ValidateModel]
        [HttpPost]
        public async Task<ActionResult<EntradaContable>> PostEntradaContable(EntradaContable entradaContable)
        {
            //fecha automatica by ignacio
            entradaContable.Fecha = DateTime.Now;

            //moneda automatica
            entradaContable.MonedaId = (entradaContable.MonedaId > 0) ? entradaContable.MonedaId : 1;
            var moneda = await _context.Moneda.FindAsync(entradaContable.MonedaId);

            if (!ValidCreditsAndDebitsAmounts(entradaContable))
            {
                return BadRequest("Esta entrada contable está descuadrada.");
            }

            if (!ValidMoveTypes(entradaContable))
            {
                return BadRequest("Esta entrada contable le hace falta un débito o crédito.");
            }

            //monto automatica by ignacio
            entradaContable.Monto = (entradaContable.Monto > 0) ? 
                entradaContable.Monto : 
                entradaContable.DetalleEntradaContable.Sum(d => d.Monto) * moneda.Tasa;

            _context.EntradaContable.Add(entradaContable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntradaContable", new { id = entradaContable.Id }, entradaContable);
        }

        private bool ValidCreditsAndDebitsAmounts(EntradaContable entrada)
        {
            if (entrada != null && entrada.DetalleEntradaContable?.Count > 0)
            {
                double? montosDbs = entrada.DetalleEntradaContable?.Where(d => d.TipoMovimiento == "DB")?.Sum(d => d.Monto);
                double? montosCrs = entrada.DetalleEntradaContable?.Where(d => d.TipoMovimiento == "CR")?.Sum(d => d.Monto);

                if (montosCrs < montosDbs)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private bool ValidMoveTypes(EntradaContable entradaContable)
        {
            if (entradaContable != null && entradaContable.DetalleEntradaContable?.Count > 0)
            {
                int? cantidadDbs = entradaContable.DetalleEntradaContable?.Where(d => d.TipoMovimiento == "DB")?.Count();
                int? cantidadCds = entradaContable.DetalleEntradaContable?.Where(d => d.TipoMovimiento == "CR")?.Count();

                if ((cantidadDbs != null && cantidadDbs >= 1) && (cantidadCds != null && cantidadCds >= 1))
                {
                    return true;
                }

                return false;
            }

            return false;
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
