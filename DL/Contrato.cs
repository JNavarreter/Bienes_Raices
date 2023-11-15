using System;
using System.Collections.Generic;

namespace DL;

public partial class Contrato
{
    public string NumeroContrato { get; set; } = null!;

    public DateTime? FechaInicioContrato { get; set; }

    public DateTime? FechaFinContrato { get; set; }

    public int IdCliente { get; set; }

    public int IdCosto { get; set; }

    public byte IdEstatusContrato { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Costo IdCostoNavigation { get; set; } = null!;

    public virtual EstatusContrato IdEstatusContratoNavigation { get; set; } = null!;

    public virtual ICollection<Ubicacion> Ubicacions { get; set; } = new List<Ubicacion>();
}
