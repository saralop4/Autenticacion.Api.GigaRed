using System.Text.Json.Serialization;


namespace Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;

public class IniciarSesionDto
{
    [JsonPropertyName("Correo")]
    public string Correo { get; set; } = null!;

    [JsonPropertyName("Contraseña")]
    public string Contraseña { get; set; } = null!;
}
