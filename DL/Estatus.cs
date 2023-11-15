using System;
using System.Collections.Generic;

namespace DL;

public partial class Estatus
{
    public byte IdEstatus { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Ubicacion> Ubicacions { get; set; } = new List<Ubicacion>();
}
