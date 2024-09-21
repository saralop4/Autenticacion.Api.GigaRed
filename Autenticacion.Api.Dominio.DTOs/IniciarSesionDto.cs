using System.ComponentModel;

namespace Autenticacion.Api.Dominio.DTOs
{
    public class IniciarSesionDto
    {
        public string Conexion { get; set; } = null!;

        public string Correo { get; set; } = null!;

        [PasswordPropertyText]
        public string Contraseña { get; set; } = null!;
    }
}
