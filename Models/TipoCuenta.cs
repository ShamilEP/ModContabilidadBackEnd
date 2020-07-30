using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModContabilidad.Models
{
    public partial class TipoCuenta
    {
        public TipoCuenta()
        {
            CuentaContable = new List<CuentaContable>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Introduzca el tipo de cuenta.")]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(2, ErrorMessage = "Introduzca un origen válido.", MinimumLength = 2)]
        public string Origen { get; set; }

        public bool Estado { get; set; }

        public virtual ICollection<CuentaContable> CuentaContable { get; set; }
    }
}
