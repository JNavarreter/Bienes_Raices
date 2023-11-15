using System;
using System.Collections.Generic;

namespace DL;

public partial class Pago
{
    public int IdPago { get; set; }

    public double Enganche { get; set; }

    public string? DiasPago { get; set; }

    public byte IdMetodoPago { get; set; }

    public double Intereses { get; set; }

    public double MensualidadMinima { get; set; }

    public virtual ICollection<Costo> Costos { get; set; } = new List<Costo>();

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;
}
