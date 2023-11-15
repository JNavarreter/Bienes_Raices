using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Costo
	{
        public int IdCosto { get; set; }

        [StringLength(20, ErrorMessage = "Letras solo puede ser menor ni mayor a 20 numeros")]
        public string? Letras { get; set; }

        [Required(ErrorMessage = "El campo Costo Total no puede estar vacio")]
        [DataType(DataType.Currency)]
        public double CostoTotal { get; set; }

        [Required(ErrorMessage = "El campo Total x Metro no puede estar vacio")]
        [DataType(DataType.Currency)]
        public double TotalxMetro { get; set; }

        [Required(ErrorMessage = "El campo Costo x Metro no puede estar vacio")]
        [DataType(DataType.Currency)]
        public double CostoxMetro { get; set; }

        public ML.Pago? Pago { get; set; }

        public List<object>? Costos { get; set; }
    }
}

