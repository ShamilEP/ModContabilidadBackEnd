using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModContabilidad.Models
{
    public partial class Moneda
    {
        public Moneda()
        {
            EntradaContable = new List<EntradaContable>();
        }

        public int Id { get; set; }

        //codigo agregado by ignacio
        [Required]
        [StringLength(10, ErrorMessage = "Introduzca el codigo.", MinimumLength = 1)]
        public string Codigo{ get; set; }

        [Required]
        [StringLength(130, ErrorMessage = "Introduzca la moneda.", MinimumLength = 2)]
        public string Descripcion { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "RD {0:C2}"), Range(0, 99.99, ErrorMessage = "Introduzca una tasa de cambio válida.")]
        public double Tasa { get; set; }
        
        public bool? Estado { get; set; }

        public virtual ICollection<EntradaContable> EntradaContable { get; set; }
    }
}
