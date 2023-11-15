using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Cliente
	{
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El campo Nombre no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede ser mayor a 50")]
        [RegularExpression("^[a-zA-Z\\s\\.]{3,50}$", ErrorMessage = "solo letras")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Apellido Paterno no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede ser mayor a 50")]
        [RegularExpression("^[a-zA-Z_-]{3,50}$", ErrorMessage = "sin espacion y solo letras")]
        public string ApellidoPaterno { get; set; } = null!;

        [Required(ErrorMessage = "El campo Apellido Materno no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede ser mayor a 50")]
        [RegularExpression("^[a-zA-Z_-]{3,50}$", ErrorMessage = "sin espacion y solo letras")]
        public string ApellidoMaterno { get; set; } = null!;

        [Range(18, 80, ErrorMessage = "Solo puede ingresar mayor 18 y menor a 80")]
        public int? Edad { get; set; }

        [Required(ErrorMessage = "El campo Telefono no puede estar vacio")]
        [StringLength(10, ErrorMessage = "El Telefono solo puede ser menor 10 numeros", MinimumLength = 10)]
        [RegularExpression("^\\+?[1-9][0-9]{9}$", ErrorMessage = "sin espacion ni letras")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; } = null!;

        [StringLength(50, ErrorMessage = "El Observaciones solo puede ser mayor a 50 letras")]
        public string? Observaciones { get; set; }

        public ML.Vendedor? Vendedor { get; set; }

        public ML.Direccion? Direccion { get; set; }

        public ML.Contrato? Contrato { get; set; }

        public List<object>? Clientes { get; set; }
    }
}