using System.Text.Json.Serialization;


namespace Autenticacion.Api.Dominio.DTOs.UsuarioDTOS
{
    public class IniciarSesionDto
    {
        [JsonPropertyName("Correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("Contraseña")]
        public string Contraseña { get; set; } = null!;
    }
}
