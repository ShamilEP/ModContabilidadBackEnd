using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModContabilidad.Models;

namespace ModContabilidad.Controllers
{
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
