using System;
using System.Collections.Generic;

namespace Autenticacion.Api.Dominio.Persistencia.Modelos;

public partial class MenuPagina
{
    public long IdMenuPagina { get; set; }

    public long IdMenuAplicacion { get; set; }

    public string? Nombre { get; set; }

    public string? Ruta { get; set; }

    public string? Icono { get; set; }

    public bool? EstadoEliminado { get; set; }

    public string UsuarioQueRegistra { get; set; } = null!;

    public string? UsuarioQueActualiza { get; set; }

    public DateOnly FechaDeRegistro { get; set; }

    public TimeOnly HoraDeRegistro { get; set; }

    public string IpDeRegistro { get; set; } = null!;

    public DateOnly? FechaDeActualizado { get; set; }

    public TimeOnly? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }

    public virtual MenuAplicacione IdMenuAplicacionNavigation { get; set; } = null!;

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
