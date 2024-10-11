namespace Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs
{
    public class UsuarioDto
    {
        public long IdUsuario { get; set; }   
        public long IdRol {  get; set; }
        public string Rol {get; set;}
        public string Correo { get; set;}
        public string Contraseña { get; set;}   
    }
}
