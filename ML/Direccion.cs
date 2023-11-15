using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Direccion
	{
        public int IdDireccion { get; set; }

        [Required(ErrorMessage = "El campo Calle no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Calle no puede ser mayor a 50")]
        public string Calle { get; set; } = null!;

        [StringLength(50, ErrorMessage = "El Numero Interior no puede ser mayor a 50")]
        public string? NumeroInterior { get; set; }

        [Required(ErrorMessage = "El campo Numeroexterior no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Numeroexterior no puede ser mayor a 50")]
        public string Numeroexterior { get; set; } = null!;

        public ML.Cliente? Cliente { get; set; }

        public List<object>? Direcciones { get; set; }
    }
}

