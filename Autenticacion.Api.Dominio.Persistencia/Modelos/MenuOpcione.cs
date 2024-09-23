namespace Autenticacion.Api.Dominio.Persistencia.Modelos;

public partial class MenuOpcione
{
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

    public virtual ICollection<MenuAplicacione> MenuAplicaciones { get; set; } = new List<MenuAplicacione>();
}
