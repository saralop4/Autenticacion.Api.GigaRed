using Autenticacion.Web.Api.Dominio.DTOs;

namespace Autenticacion.Web.Api.Dominio.Interfaces
{
    public interface IMenuRepositorio
    {
        public Task<List<MenuDto>> ObtenerMenusPorRol(long IdRol);
    }
}
