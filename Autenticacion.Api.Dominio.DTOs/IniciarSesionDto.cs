using System.ComponentModel;
using System.Text.Json.Serialization;


namespace Autenticacion.Api.Dominio.DTOs
{
    public class IniciarSesionDto
    {
        [JsonPropertyName("Correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("Contraseña")]
        [PasswordPropertyText]
        public string Contraseña { get; set; } = null!;
    }
}
