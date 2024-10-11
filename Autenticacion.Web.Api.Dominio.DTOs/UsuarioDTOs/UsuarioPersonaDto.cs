using System.Text.Json.Serialization;

namespace Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;

public class UsuarioPersonaDto
{

    [JsonPropertyName("IdIndicativo")]
    public long IdIndicativo { get; set; }

    [JsonPropertyName("IdCiudad")]
    public long IdCiudad { get; set; }

    [JsonPropertyName("PrimerNombre")]
    public string PrimerNombre { get; set; }

    [JsonPropertyName("SegundoNombre")]
    public string? SegundoNombre { get; set; } = null;

    [JsonPropertyName("PrimerApellido")]
    public string PrimerApellido { get; set; }

    [JsonPropertyName("SegundoApellido")]
    public string? SegundoApellido { get; set; } = null;

    [JsonPropertyName("Telefono")]
    public string Telefono { get; set; }

    [JsonPropertyName("NombreFoto")]
    public string? NombreFoto { get; set; } = null;

    [JsonPropertyName("UsuarioQueRegistraPersona")]
    public string UsuarioQueRegistraPersona { get; set; }

    [JsonPropertyName("IpDeRegistroPersona")]
    public string IpDeRegistroPersona { get; set; }

    //*** Propiedades de Usuario

    [JsonPropertyName("IdRol")]
    [JsonIgnore]
    public long IdRol { get; set; } = 1;

    [JsonPropertyName("Correo")]
    public string Correo { get; set; } = null!;

    [JsonPropertyName("Contraseña")]
    public string Contraseña { get; set; } = null!;

    [JsonPropertyName("UsuarioQueRegistraUsuario")]
    public string UsuarioQueRegistraUsuario { get; set; } = null!;

    [JsonIgnore]
    public string IpDeRegistroUsuario { get; set; } = null!;

}
