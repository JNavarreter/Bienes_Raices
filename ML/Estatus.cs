using System;
namespace ML
{
	public class Estatus
	{
        public Estatus()
        {

        }
        public byte IdEstatus { get; set; }

        public string Nombre { get; set; } = null!;

        public List<object>? Estatuses { get; set; }
    }
}

