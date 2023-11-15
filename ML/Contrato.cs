using System;
using System.ComponentModel.DataAnnotations;

namespace ML
{
	public class Contrato
	{
        [Required(ErrorMessage = "El campo Celular no puede estar vacio")]
        [StringLength(20, ErrorMessage = "El Celular solo puede ser menor ni mayor a 20 numeros")]
        public string NumeroContrato { get; set; } = null!;

        [DataType(DataType.Date)]
        public string FechaInicioContrato { get; set; } = null!;

        [DataType(DataType.Date)]
        public string FechaFinContrato { get; set; } = null!;

        public ML.Cliente? Cliente { get; set; }

        public ML.Costo? Costo { get; set; }

        public ML.EstatusContrato? EstatusContrato { get; set; }

        public ML.Ubicacion? Ubicacion { get; set; }

        public List<object>? NumeroContratos { get; set; }
    }
}

