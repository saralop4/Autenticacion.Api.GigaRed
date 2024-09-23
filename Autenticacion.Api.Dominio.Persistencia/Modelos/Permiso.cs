namespace Autenticacion.Api.Dominio.Persistencia.Modelos;

public partial class Permiso
{
    public long IdPermiso { get; set; }

    public long IdMenuPagina { get; set; }

    public long IdRol { get; set; }

    public bool? EstadoEliminado { get; set; }

    public string UsuarioQueRegistra { get; set; } = null!;

    public string? UsuarioQueActualiza { get; set; }

    public DateOnly FechaDeRegistro { get; set; }

    public TimeOnly HoraDeRegistro { get; set; }

    public string IpDeRegistro { get; set; } = null!;

    public DateOnly? FechaDeActualizado { get; set; }

    public TimeOnly? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }

    public virtual MenuPagina IdMenuPaginaNavigation { get; set; } = null!;
}
