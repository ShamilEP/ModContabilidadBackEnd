using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModContabilidad.Models
{
    public partial class DetalleEntradaContable
    {
        public DetalleEntradaContable()
        {
            Estado = true;
        }

        public int Id { get; set; }
        public int EntradaContableId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Introduzca una cuenta contable válida.")]
        public int CuentaContableId { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Introduzca un tipo de movimiento válido.", MinimumLength = 2)]
        public string TipoMovimiento { get; set; }
        //descripcion de detalle agregada by ignacio
        [Required]
        [StringLength(130, ErrorMessage = "Descripción inválida.", MinimumLength = 1)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Introduzca un monto válido.")]
        public double Monto { get; set; }

        public bool Estado { get; set; }

        public virtual CuentaContable CuentaContable { get; set; }
        public virtual EntradaContable EntradaContable { get; set; }
    }
}
