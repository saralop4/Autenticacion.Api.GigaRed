using Autenticacion.Api.Dominio.DTOs;

namespace Autenticacion.Api.Dominio.Interfaces
{
    public interface IMenuRepositorio
    {
       public Task<List<MenuDto>> ObtenerMenusPorRol(long IdRol);
    }
}
