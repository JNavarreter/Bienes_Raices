using System;
using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Pago
	{
        public int IdPago { get; set; }

        [Required(ErrorMessage = "El campo Enganche no puede estar vacio")]
        [DataType(DataType.Currency)]
        public double Enganche { get; set; }

        [Required(ErrorMessage = "El campo Dias Pago no puede estar vacio")]
        [StringLength(2, ErrorMessage = "El Dias Pago no puede ser mayor a 2")]
        public string? DiasPago { get; set; }

        public ML.MetodoPago? MetodoPago { get; set; }

        [Required(ErrorMessage = "El campo Enganche no puede estar vacio")]
        [DataType(DataType.Currency)]
        public double Intereses { get; set; }

        [Required(ErrorMessage = "El campo Enganche no puede estar vacio")]
        [DataType(DataType.Currency)]
        public double MensualidadMinima { get; set; }

        public List<object>? Pagos { get; set; }
    }
}

