using System;
namespace ML
{
	public class MetodoPago
	{
        public MetodoPago()
        {

        }
        public byte IdMetodoPago { get; set; }

        public string Nombre { get; set; } = null!;

        public List<object>? MetodosPago { get; set; }
    }
}

