using System.Text.Json.Serialization;

namespace Autenticacion.Api.Dominio.DTOs.PersonaDTOS
{
    public class PersonaDto
    {
        [JsonIgnore]
        [JsonPropertyName("IdPersona")]
        public long IdPersona { get; set; }

        [JsonPropertyName("IdIndicativo")]
        public long IdIndicativo { get; set; }  

        [JsonPropertyName("PrimerNombre")]
        public string PrimerNombre { get; set; } = null!;

        [JsonPropertyName("SegundoNombre")]
        public string? SegundoNombre { get; set; } = null;

        [JsonPropertyName("PrimerApellido")]
        public string PrimerApellido { get; set; } = null!;

        [JsonPropertyName("SegundoApellido")]
        public string? SegundoApellido { get; set; } = null;

        [JsonPropertyName("Telefono")]
        public decimal Telefono { get; set; }

        [JsonPropertyName("Foto")]
        public byte[]? Foto { get; set; } = null;

        [JsonPropertyName("NombreFoto")]
        public string? NombreFoto { get; set; } = null;

        [JsonPropertyName("EstadoEliminado")]
        public bool? EstadoEliminado { get; set; } = false;

        [JsonPropertyName("UsuarioQueRegistra")]
        public string UsuarioQueRegistra { get; set; } = null!;

        [JsonPropertyName("UsuarioQueActualiza")]
        public string? UsuarioQueActualiza { get; set; } = null;

        [JsonIgnore]
        [JsonPropertyName("FechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; } 

        [JsonIgnore]
        [JsonPropertyName("HoraDeRegistro")]
        public TimeSpan HoraDeRegistro { get; set; }

        [JsonPropertyName("IpDeRegistro")]
        public string? IpDeRegistro { get; set; } = null;

        [JsonPropertyName("FechaDeActualizado")]
        public DateTime? FechaDeActualizado { get; set; } = null;

        [JsonPropertyName("HoraDeActualizado")] 
        public TimeSpan? HoraDeActualizado { get; set; } = null;

        [JsonPropertyName("IpDeActualizado")]
        public string? IpDeActualizado { get; set; } = null;
    }
}
