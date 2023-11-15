using System;
using System.Collections.Generic;

namespace DL;

public partial class Ubicacion
{
    public int IdUbicacion { get; set; }

    public string Desarrollo { get; set; } = null!;

    public string Manzana { get; set; } = null!;

    public string Lote { get; set; } = null!;

    public byte IdEstatus { get; set; }

    public string NumeroContrato { get; set; } = null!;

    public virtual Estatus IdEstatusNavigation { get; set; } = null!;

    public virtual Contrato NumeroContratoNavigation { get; set; } = null!;
}
