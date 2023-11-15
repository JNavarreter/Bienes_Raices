using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ML
{
	public class Usuario
	{
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo Username no puede estar vacio")]
        [StringLength(20, ErrorMessage = "El Username no puede ser menor a 8 ni mayor a 20", MinimumLength = 8)]
        [RegularExpression("^[a-zA-Z][a-zA-Z\\d-_\\.]+$", ErrorMessage = "Sin espacion")]
        public string Username { get; set; } = null!;

        [AllowNull]
        public byte[] Password { get; set; } = null!;

        public bool Estatus { get; set; }

        public ML.Vendedor? Vendedor { get; set; }

        public ML.Rol? Rol { get; set; }

        public List<object>? Usuarios { get; set; }
    }
}

