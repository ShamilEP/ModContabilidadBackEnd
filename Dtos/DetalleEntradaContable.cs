using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModContabilidad.Dtos
{
    [Serializable]
    public partial class DetalleEntradaContable
    {
        public int CuentaContableId { get; set; }
        public string TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public double Monto { get; set; }
    }
}
