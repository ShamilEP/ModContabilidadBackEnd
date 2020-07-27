using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModContabilidad.Models
{
    public partial class EntradaContable
    {
        public EntradaContable()
        {
            DetalleEntradaContable = new List<DetalleEntradaContable>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(130, ErrorMessage = "Introduzca una descripción válida.", MinimumLength = 5)]
        public string Descripcion { get; set; }

        //fecha by ignacio
        [Required]
        public DateTime Fecha { get; set; }

        //monto by ignacio
        [Required]
        public double Monto { get; set; }

        [Required]
        [Range(1, 9, ErrorMessage = "Introduzca un auxiliar válido.")]
        public int AuxiliarId { get; set; }

        [Required]
        [Range(0, 244, ErrorMessage = "Introduzca una moneda válida.")]
        public int MonedaId { get; set; }

        public bool Estado { get; set; }

        public virtual Auxiliar Auxiliar { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual ICollection<DetalleEntradaContable> DetalleEntradaContable { get; set; }
    }
}
