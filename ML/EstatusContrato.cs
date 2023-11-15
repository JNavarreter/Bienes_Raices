using System;
namespace ML
{
	public class EstatusContrato
	{
        public EstatusContrato()
        {

        }
        public byte IdEstatusContrato { get; set; }

        public string Nombre { get; set; } = null!;

        public List<object>? EstatusContratos { get; set; }
    }
}

