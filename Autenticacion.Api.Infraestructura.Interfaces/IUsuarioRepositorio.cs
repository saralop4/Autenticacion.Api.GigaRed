using Autenticacion.Api.Dominio.DTOs.UsuarioDTOS;

namespace Autenticacion.Api.Infraestructura.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<UsuarioDto>
    {
        public Task<UsuarioDto> UsuarioAutenticado(IniciarSesionDto IniciarSesionDto);
    }
}
