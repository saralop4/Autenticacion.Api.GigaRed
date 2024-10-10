using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;

namespace Autenticacion.Web.Api.Dominio.Interfaces
{
    public interface IUsuarioRepositorio 
    {
        public Task<UsuarioDto> Guardar(UsuarioDto Modelo);
        public Task<UsuarioDto> Obtener(string Id);
        public Task<UsuarioDto> ValidarUsuario(IniciarSesionDto IniciarSesionDto);
        public Task<bool> ExisteIdPersona(long Id);
    }
}
