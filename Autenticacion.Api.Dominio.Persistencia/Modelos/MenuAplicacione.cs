namespace Autenticacion.Api.Dominio.Persistencia.Modelos;

public partial class MenuAplicacione
{
    public long IdMenuAplicacion { get; set; }

    public long IdMenuOpcion { get; set; }

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

    public virtual MenuOpcione IdMenuOpcionNavigation { get; set; } = null!;

    public virtual ICollection<MenuPagina> MenuPaginas { get; set; } = new List<MenuPagina>();
}
