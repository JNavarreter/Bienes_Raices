using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusContrato
{
    public byte IdEstatusContrato { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
