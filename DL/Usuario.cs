using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Username { get; set; } = null!;

    public byte[]? Password { get; set; }

    public bool? Estatus { get; set; }

    public int IdVendedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Curp { get; set; } = null!;

    public string? Rfc { get; set; }

    public string? Foto { get; set; }

    public string Email { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public byte IdRol { get; set; }

    public string NombreRol { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Vendedor IdVendedorNavigation { get; set; } = null!;
}
