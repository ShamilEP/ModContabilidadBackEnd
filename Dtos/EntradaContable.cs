using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModContabilidad.Dtos
{
    [Serializable]
    public partial class EntradaContable
    {
        public EntradaContable()
        {
            DetalleEntradaContable = new List<ModContabilidad.Dtos.DetalleEntradaContable>();
        }

        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public int AuxiliarId { get; set; }
        public int MonedaId { get; set; }
        public virtual ICollection<ModContabilidad.Dtos.DetalleEntradaContable> DetalleEntradaContable { get; set; }
    }
}
