using System;
using System.Collections.Generic;

namespace DL;

public partial class Costo
{
    public int IdCosto { get; set; }

    public string? Letras { get; set; }

    public double CostoTotal { get; set; }

    public double TotalxMetro { get; set; }

    public double CostoxMetro { get; set; }

    public int IdPago { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual Pago IdPagoNavigation { get; set; } = null!;
}
