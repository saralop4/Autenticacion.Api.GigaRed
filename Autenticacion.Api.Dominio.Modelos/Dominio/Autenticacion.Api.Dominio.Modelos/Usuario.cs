﻿using System;
using System.Collections.Generic;

namespace Autenticacion.Api.Dominio.Modelos.Dominio.Autenticacion.Api.Dominio.Modelos;

public partial class Usuario
{
    public long IdUsuario { get; set; }

    public long IdRol { get; set; }

    public long IdPersona { get; set; }

    public string Correo { get; set; } = null!;

    public string? Contraseña { get; set; }

    public bool? EstadoEliminado { get; set; }

    public string UsuarioQueRegistra { get; set; } = null!;

    public string? UsuarioQueActualiza { get; set; }

    public DateOnly FechaDeRegistro { get; set; }

    public TimeOnly? HoraDeRegistro { get; set; }

    public string IpDeRegistro { get; set; } = null!;

    public DateOnly? FechaDeActualizado { get; set; }

    public TimeOnly? HoraDeActualizado { get; set; }

    public string? IpDeActualizado { get; set; }

    public virtual Persona IdPersonaNavigation { get; set; } = null!;
}
