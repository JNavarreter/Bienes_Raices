using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Vendedor
	{
        public int IdVendedor { get; set; }

        [Required(ErrorMessage = "El campo Nombre no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede ser mayor a 50")]
        [RegularExpression("^[a-zA-Z\\s\\.]{3,50}$", ErrorMessage = "Solo letras")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Apellido Paterno no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede ser mayor a 50")]
        [RegularExpression("^[a-zA-Z_-]{3,50}$", ErrorMessage = "sin espacion y solo letras")]
        public string ApellidoPaterno { get; set; } = null!;

        [Required(ErrorMessage = "El campo Apellido Materno no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede ser mayor a 50")]
        [RegularExpression("^[a-zA-Z_-]{3,50}$", ErrorMessage = "sin espacion y solo letras")]
        public string ApellidoMaterno { get; set; } = null!;

        [Required(ErrorMessage = "El campo CURP no puede estar vacio")]
        [StringLength(18, ErrorMessage = "El CURP solo pueden ser 18 caracteres", MinimumLength = 18)]
        [RegularExpression("^[A-Z0-9_-]{18,18}$", ErrorMessage = "sin espacion y solo letras")]
        public string Curp { get; set; } = null!;

        [StringLength(13, ErrorMessage = "El RFC solo no pueden ser 13 caracteres", MinimumLength = 13)]
        [RegularExpression("^[A-Z0-9_-]{13,13}$", ErrorMessage = "sin espacion y solo letras")]
        public string? Rfc { get; set; }

        public string? Foto { get; set; }

        [Required(ErrorMessage = "El campo Email no puede estar vacio")]
        [StringLength(50, ErrorMessage = "El ApellidoMaterno no puede ser mayor a 50")]
        [EmailAddress(ErrorMessage = "El Email que ingreso no es valido")]
        [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "El Email que ingreso no es valido")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El campo Celular no puede estar vacio")]
        [StringLength(10, ErrorMessage = "El Celular solo puede ser menor 10 numeros", MinimumLength = 10)]
        [RegularExpression("^\\+?[1-9][0-9]{9}$", ErrorMessage = "sin espacion ni letras")]
        [DataType(DataType.PhoneNumber)]
        public string Celular { get; set; } = null!;

        public List<object>? Vendedores { get; set; }
    }
}

