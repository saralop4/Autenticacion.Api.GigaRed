using System;
using System.Collections.Generic;

namespace Autenticacion.Api.Dominio.Persistencia.Modelos;

public partial class Persona
{
    public long IdPersona { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public decimal Telefono { get; set; }

    public byte[]? Foto { get; set; }

    public string? NombreFoto { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
