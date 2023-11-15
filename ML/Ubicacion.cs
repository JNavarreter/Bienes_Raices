using System;
using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Ubicacion
	{
        public int IdUbicacion { get; set; }

        [Required(ErrorMessage = "El campo Calle no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Calle no puede ser mayor a 50")]
        public string Desarrollo { get; set; } = null!;

        [Required(ErrorMessage = "El campo Calle no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Calle no puede ser mayor a 50")]
        public string Manzana { get; set; } = null!;

        [Required(ErrorMessage = "El campo Calle no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Calle no puede ser mayor a 50")]
        public string Lote { get; set; } = null!;

        public ML.Contrato? Contrato { get; set; }

        public ML.Estatus? Estatus { get; set; }

        public List<object>? Ubicacaciones { get; set; }
    }
}