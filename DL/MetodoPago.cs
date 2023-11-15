using System;
using System.Collections.Generic;

namespace DL;

public partial class MetodoPago
{
    public byte IdMetodoPago { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
