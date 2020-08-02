using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModContabilidad.Models;

namespace ModContabilidad.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuxiliaresController : ControllerBase
    {
        private readonly sistema_contabilidadContext _context;

        public AuxiliaresController(sistema_contabilidadContext context)
        {
            _context = context;
        }

        // GET: api/Auxiliares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Auxiliar>>> GetAuxiliar()
        {
            return await _context.Auxiliar.ToListAsync();
        }
    }
}
