using System;
using System.Collections.Generic;

namespace DL;

public partial class Vendedor
{
    public int IdVendedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Curp { get; set; } = null!;

    public string? Rfc { get; set; }

    public string? Foto { get; set; }

    public string Email { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
