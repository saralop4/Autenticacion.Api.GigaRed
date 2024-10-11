using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;

namespace Autenticacion.Web.Api.Dominio.Interfaces
{
    public interface IUsuarioRepositorio 
    {
        public Task<bool> Guardar(UsuarioPersonaDto Modelo);
        public Task<UsuarioDto> Obtener(string Id);
        public Task<UsuarioDto> ValidarUsuario(IniciarSesionDto IniciarSesionDto);
    }
}
