using Autenticacion.Web.Api.Dominio.DTOs;

namespace Autenticacion.Web.Api.Dominio.Interfaces;
public interface IIndicativoRepositorio
{
    Task<IEnumerable<IndicativoDto>> ObtenerTodo();


}
