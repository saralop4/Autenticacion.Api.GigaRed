using Autenticacion.Web.Api.Dominio.DTOs.UsuarioDTOs;

namespace Autenticacion.Web.Api.Dominio.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<UsuarioDto>
    {
        public Task<UsuarioDto> UsuarioAutenticado(IniciarSesionDto IniciarSesionDto);
        public Task<bool> ExisteIdPersona(long Id);
    }
}
