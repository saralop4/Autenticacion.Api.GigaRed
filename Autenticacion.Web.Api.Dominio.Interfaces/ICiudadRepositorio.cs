using Autenticacion.Web.Api.Dominio.DTOs;

namespace Autenticacion.Web.Api.Dominio.Interfaces;
public interface ICiudadRepositorio
{
    Task<IEnumerable<CiudadDto>> ObtenerTodo();    
}
