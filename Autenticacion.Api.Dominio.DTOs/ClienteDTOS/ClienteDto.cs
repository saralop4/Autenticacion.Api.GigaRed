using System.Text.Json.Serialization;

namespace Autenticacion.Api.Dominio.DTOs.ClienteDTOS
{
    public class ClienteDto
    {

        [JsonIgnore]
        public long IdCliente { get; set; }

        [JsonPropertyName("IdPersona")]
        public long IdPersona { get; set; }

        public bool Estado {  get; set; }   

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
        public string? IpDeActualizado { get; set; } = null;
    }
}
