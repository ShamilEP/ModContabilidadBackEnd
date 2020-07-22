using System;
using System.Collections.Generic;

namespace ModContabilidad.Models
{
    public partial class Auxiliar
    {
        public Auxiliar()
        {
            EntradaContable = new List<EntradaContable>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<EntradaContable> EntradaContable { get; set; }
    }
}
