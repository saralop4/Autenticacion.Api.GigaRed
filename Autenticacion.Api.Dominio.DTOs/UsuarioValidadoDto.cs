namespace Autenticacion.Api.Dominio.DTOs
{
    public class UsuarioValidadoDto
    {
        public long IdPersona { get; set; } = 0!;
        public string Correo { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string UsuarioQueRegistra { get; set; } = null!;

    }
}
