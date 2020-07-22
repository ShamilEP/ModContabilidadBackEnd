using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModContabilidad.Models
{
    public partial class CuentaContable
    {
        public CuentaContable()
        {
            DetalleEntradaContable = new List<DetalleEntradaContable>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(130, ErrorMessage = "Introduzca la cuenta contable.", MinimumLength = 5)]
        public string Descripcion { get; set; }

        [Required]
        public bool PermiteTransaccion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca un tipo de cuenta válido.")]
        public int TipoCuentaId { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = "Introduzca un nivel válido.")]
        public int Nivel { get; set; }

        public int? CuentaMayor { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Introduzca un balance válido.")]
        public double Balance { get; set; }

        public bool Estado { get; set; }

        public virtual TipoCuenta TipoCuenta { get; set; }
        public virtual ICollection<DetalleEntradaContable> DetalleEntradaContable { get; set; }
    }
}
