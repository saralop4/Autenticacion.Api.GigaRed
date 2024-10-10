using Autenticacion.Web.Api.Dominio.DTOs;
using Autenticacion.Web.Api.Transversal.Modelos;

namespace Autenticacion.Web.Api.Aplicacion.Interfaces;

public interface ICiudadServicio
{
    Task<Response<IEnumerable<CiudadDto>>> ObtenerTodos();


}
