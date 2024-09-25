using System.Text.Json.Serialization;

namespace Autenticacion.Api.Dominio.DTOs
{
    public class UsuarioDto
    {
        [JsonIgnore]
        [JsonPropertyName("IdUsuario")]
        public long IdUsuario { get; set; }

        [JsonPropertyName("IdRol")]
        public long IdRol { get; set; } = 1;

        [JsonPropertyName("IdPersona")]
        public long IdPersona { get; set; }

        [JsonPropertyName("Correo")]
        public string Correo { get; set; } = null!;

        [JsonPropertyName("Contraseña")]
        public string Contraseña { get; set; } = null!;

        [JsonPropertyName("EstadoEliminado")]
        public bool? EstadoEliminado { get; set; } = false;

        [JsonPropertyName("UsuarioQueRegistra")]
        public string UsuarioQueRegistra { get; set; } = null!;

        [JsonPropertyName("UsuarioQueActualiza")]
        public string? UsuarioQueActualiza { get; set; }

        [JsonIgnore]
        [JsonPropertyName("FechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; }

        [JsonIgnore]
        [JsonPropertyName("HoraDeRegistro")]
        public TimeSpan HoraDeRegistro { get; set; }

        [JsonIgnore]
        [JsonPropertyName("IpDeRegistro")]
        public string? IpDeRegistro { get; set; } = null;

        [JsonPropertyName("FechaDeActualizado")]
        public DateTime? FechaDeActualizado { get; set; } = null;

        [JsonPropertyName("HoraDeActualizado")]
        public TimeSpan? HoraDeActualizado { get; set; } = null;

        [JsonIgnore]
        [JsonPropertyName("IpDeActualizado")]
        public string? IpDeActualizado { get; set; }= null;
    }
}
